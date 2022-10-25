package com.ssafy.b305.controller;

import com.ssafy.b305.service.BookService;
import io.swagger.annotations.ApiResponse;
import io.swagger.annotations.ApiResponses;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

@ApiResponses({
        @ApiResponse(code = 200, message = "성공"),
        @ApiResponse(code = 401, message = "인증 실패"),
        @ApiResponse(code = 404, message = "사용자 없음"),
        @ApiResponse(code = 500, message = "서버 오류")
})

@RestController
@RequestMapping("/book")
@CrossOrigin
public class BookController {

    @Autowired
    BookService bookService;

    @GetMapping("/{title}")
    public ResponseEntity<?> getScripts(@PathVariable String title) {
        String result = bookService.getScripts(title);

        if(result != "") {
            return new ResponseEntity<String>(result, HttpStatus.OK);
        }
        return new ResponseEntity<String>("Fail", HttpStatus.NO_CONTENT);
    }

}
