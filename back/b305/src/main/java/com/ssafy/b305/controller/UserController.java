package com.ssafy.b305.controller;

import com.ssafy.b305.service.MailService;
import com.ssafy.b305.service.UserService;
import io.swagger.annotations.ApiResponse;
import io.swagger.annotations.ApiResponses;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.sql.SQLException;

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

    final String SUCCESS = "success";
    final String FAIL = "fail";

    @Autowired
    UserService userService;

    @Autowired
    MailService mailService;

    @GetMapping("/mail")
    public ResponseEntity<String> sendMail(@RequestBody String email) throws SQLException {
        if(userService.findUserById(email) != null && userService.makeTmpPw(email) != "") {
            return new ResponseEntity<String>(SUCCESS, HttpStatus.OK);
        }
        return new ResponseEntity<String>(FAIL, HttpStatus.NOT_FOUND);
    }

}
