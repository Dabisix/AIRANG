package com.ssafy.b305.controller;

import com.ssafy.b305.annotation.NoJwt;
import com.ssafy.b305.domain.dto.TokenResponse;
import com.ssafy.b305.domain.dto.UserNewInfo;
import com.ssafy.b305.domain.entity.Auth;
import com.ssafy.b305.domain.entity.User;
import com.ssafy.b305.jwt.JwtTokenProvider;
import com.ssafy.b305.service.AuthService;

import com.ssafy.b305.service.MailService;
import com.ssafy.b305.service.UserService;
import io.swagger.annotations.ApiResponse;
import io.swagger.annotations.ApiResponses;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.sql.SQLException;
import java.util.HashMap;
import java.util.Map;

@ApiResponses({
        @ApiResponse(code = 200, message = "성공"),
        @ApiResponse(code = 401, message = "인증 실패"),
        @ApiResponse(code = 404, message = "사용자 없음"),
        @ApiResponse(code = 500, message = "서버 오류")
})

@RestController
@RequestMapping("/user")
@CrossOrigin("*")
public class UserController {

    private static final String SUCCESS = "success";
    private static final String FAIL = "fail";

    @Autowired
    UserService userService;

    @Autowired
    AuthService authService;

    @Autowired
    JwtTokenProvider jwtTokenProvider;

    //회원가입
    @NoJwt
    @PostMapping("/signup")
    public ResponseEntity<?> signup(@RequestBody User user) {
        if(userService.registUser(user))
            return new ResponseEntity<String>(SUCCESS, HttpStatus.OK);
        return new ResponseEntity<String>(FAIL, HttpStatus.NOT_ACCEPTABLE);
    }

    //로그인
    @NoJwt
    @PostMapping
    public ResponseEntity<Map<String, Object>> login(@RequestBody User user) {
        Map<String, Object> resultMap = new HashMap<>();
        HttpStatus status = null;
        String id = user.getEmail();

        try {
            String result = userService.login(user);
            if (result.equals(id)) {

                User loginUser = userService.findUserByEmail(result);
                String accessToken = jwtTokenProvider.createAccessToken(user.getEmail());
                String refreshToken = jwtTokenProvider.createRefreshToken(user.getEmail());

                Auth auth = Auth.builder()
                        .user(loginUser)
                        .refreshToken(refreshToken)
                        .build();

                if(authService.findUser(id).isPresent()) {
                    Auth alreadyAuth = authService.findUser(user.getEmail()).get();
                    authService.delete(alreadyAuth);
                }

                authService.save(auth);

                resultMap.put("token", TokenResponse.builder()
                        .ACCESS_TOKEN(accessToken)
                        .REFRESH_TOKEN(refreshToken)
                        .build());

                resultMap.put("message", SUCCESS);
                status = HttpStatus.ACCEPTED;
            } else if (result.equals("pwErr")){
                resultMap.put("message", "pwErr");
                status = HttpStatus.ACCEPTED;
            } else if (result.equals("noId")) {
                resultMap.put("message", "noId");
                status = HttpStatus.ACCEPTED;

            }
        } catch (Exception e) {
            resultMap.put("message", e.getMessage());
            status = HttpStatus.NOT_MODIFIED;
        }
        return new ResponseEntity<Map<String, Object>>(resultMap, status);
    }

    //임시 비밀번호 발급
    @NoJwt
    @PostMapping("/mail")
    public ResponseEntity<String> sendMail(@RequestBody Map<String, String> request) throws SQLException {
        String email = request.get("email");
        if(userService.findUserByEmail(email) != null && userService.makeTmpPw(email) != "") {
            return new ResponseEntity<String>("메일 전송 성공", HttpStatus.OK);
        }
        return new ResponseEntity<String>("메일 전송 실패", HttpStatus.NOT_FOUND);
    }

    //유저 정보 수정
    @PutMapping
    public ResponseEntity<String> updateInfo(@RequestHeader(value = "access-token") String request, @RequestBody UserNewInfo info) {
        String email = jwtTokenProvider.getUserID(request);

        if (userService.updateUser(email, info) == 1)
            return new ResponseEntity<String>("유저 정보 업데이트 성공", HttpStatus.OK);
        return new ResponseEntity<String>("정보 업데이트 실패", HttpStatus.NOT_FOUND);
    }

    //유저 삭제
    @PatchMapping
    public ResponseEntity<?> withdraw(@RequestHeader(value = "access-token") String request) {
        String userEmail = jwtTokenProvider.getUserID(request);

        boolean cnt = userService.deleteUser(userEmail);

        //상태 코드만으로 구분
        if (cnt) return new ResponseEntity<String>(SUCCESS, HttpStatus.OK);
        else return new ResponseEntity<String>(FAIL, HttpStatus.NOT_FOUND);
    }

    //유저정보
    @GetMapping
    public ResponseEntity<?> getInfo(@RequestHeader(value = "access-token") String request) {
        String email = jwtTokenProvider.getUserID(request);

        User user = userService.myPage(email);

        if(user != null)
            return new ResponseEntity<User>(user, HttpStatus.OK);
        return new ResponseEntity<String>(FAIL, HttpStatus.NOT_FOUND);
    }

    //이메일 중복검사
    @NoJwt
    @PostMapping("/id")
    public ResponseEntity<?> checkDuplication(@RequestBody Map<String, String> request) {
        String email = request.get("email");
        User user = userService.myPage(email);

        if(user != null)
            return new ResponseEntity<String>("이미 존재하는 email", HttpStatus.IM_USED);
        return new ResponseEntity<String>(SUCCESS, HttpStatus.OK);
    }

}
