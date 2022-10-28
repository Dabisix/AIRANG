package com.ssafy.b305.controller;

import com.ssafy.b305.annotation.NoJwt;
import com.ssafy.b305.domain.dto.TokenResponse;
import com.ssafy.b305.jwt.JwtTokenProvider;
import com.ssafy.b305.repository.AuthRepository;
import io.swagger.annotations.ApiResponse;
import io.swagger.annotations.ApiResponses;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletRequest;
import java.sql.SQLException;

@ApiResponses({
        @ApiResponse(code = 200, message = "성공"),
        @ApiResponse(code = 401, message = "인증 실패"),
        @ApiResponse(code = 404, message = "사용자 없음"),
        @ApiResponse(code = 500, message = "서버 오류")
})

@RestController
@RequestMapping("/auth")
@CrossOrigin
public class AuthController {

    @Autowired
    JwtTokenProvider jwtTokenProvider;

    @NoJwt
    @GetMapping
    public ResponseEntity<String> refreshToken(@RequestHeader(value = "refresh-token") String request) throws Exception {
        TokenResponse response = jwtTokenProvider.issueAccessToken(request);

        //Refresh Token은 유효해서 새로 Access Token을 발급한 경우
        if(response != null) {
            return new ResponseEntity<String>(response.getACCESS_TOKEN(), HttpStatus.OK);
        }
        //Refresh Token도 유통기한 지났음
        return new ResponseEntity<String>("재로그인 필요", HttpStatus.UNAUTHORIZED);

    }
}
