package com.ssafy.b305.controller;

import com.ssafy.b305.domain.entity.User;
import com.ssafy.b305.jwt.JwtTokenProvider;
import com.ssafy.b305.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.servlet.ServletOutputStream;
import java.io.BufferedInputStream;
import java.io.File;

@RestController
@RequestMapping("/ai")
@CrossOrigin
public class AiNarrationController {

    @Autowired
    UserService userService;
    @Autowired
    JwtTokenProvider jwtTokenProvider;

    @GetMapping("/{id}")
    public ResponseEntity<?> getAiNarration(@RequestHeader(value = "access-token") String request, @PathVariable Long id) {
        // check user info
        User user;
        try {
            String userEmail = jwtTokenProvider.getUserID(request);
            user = userService.myPage(userEmail);
            if (user == null) {
                throw new Exception();
            }
        } catch (Exception e) {
            return new ResponseEntity<>("unauthorized Token", HttpStatus.UNAUTHORIZED);
        }

        try{
            ServletOutputStream stream = null;
            BufferedInputStream buf = null;

            String path = "/home/ubuntu/tts/voice/ai/" + user.getEmail() + "/" + id;

            File file = new File(path);
            if (file.mkdirs()) {
                // 책 녹음 데이터 없음. fast api로 요청하기
                System.out.println("fast api 연결");
            }
        } catch (Exception e){
            return new ResponseEntity<>("ai voice 실패", HttpStatus.BAD_REQUEST);
        }
        return new ResponseEntity<>(HttpStatus.OK);
    }
}
