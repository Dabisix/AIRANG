package com.ssafy.b305.controller;

import com.ssafy.b305.domain.dto.BookInfoResponseDto;
import com.ssafy.b305.domain.dto.BookRequestDto;
import com.ssafy.b305.domain.dto.BookDetailResponseDto;
import com.ssafy.b305.domain.entity.User;
import com.ssafy.b305.jwt.JwtTokenProvider;
import com.ssafy.b305.service.BookService;
import com.ssafy.b305.service.BookInfoService;
import com.ssafy.b305.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/book")
@CrossOrigin
public class BookController {

    @Autowired
    BookService bookService;

    @Autowired
    UserService userService;
    @Autowired
    JwtTokenProvider jwtTokenProvider;

    @Autowired
    BookInfoService starService;

    @GetMapping("/{id}")
    public ResponseEntity<?> getBook(@RequestHeader(value = "access-token") String request, @PathVariable Long id) {
        User user;
        try{
            String userEmail = jwtTokenProvider.getUserID(request);
            user = userService.myPage(userEmail);
            if (user == null) {
                throw new Exception();
            }
        }catch (Exception e){
            return new ResponseEntity<>("unauthorized Token", HttpStatus.UNAUTHORIZED);
        }

        try{
            BookDetailResponseDto bookDetailResponseDto = bookService.getBook(id);
            if(bookDetailResponseDto != null){
                return new ResponseEntity<BookDetailResponseDto>(bookDetailResponseDto, HttpStatus.OK);
            }else{
                return new ResponseEntity<>("not exist book id", HttpStatus.NO_CONTENT);
            }
        }catch (Exception e){
            return new ResponseEntity<>("fail to select book id", HttpStatus.BAD_REQUEST);
        }
    }

    @PostMapping()
    public ResponseEntity<?> getBookList(@RequestHeader(value = "access-token") String request, @RequestBody BookRequestDto bookRequestDto){
        User user;
        try{
            String userEmail = jwtTokenProvider.getUserID(request);
            user = userService.myPage(userEmail);
            if (user == null) {
                throw new Exception();
            }
        }catch (Exception e){
            return new ResponseEntity<>("unauthorized Token", HttpStatus.UNAUTHORIZED);
        }

        try{
            List<BookInfoResponseDto> bookInfoResponseDtos = bookService.getBookList(bookRequestDto);
            if(bookInfoResponseDtos==null || bookInfoResponseDtos.size()==0){
                return new ResponseEntity<>("no books that satisfing condition", HttpStatus.NO_CONTENT);
            }else{
                return new ResponseEntity<>(bookInfoResponseDtos, HttpStatus.OK);
            }
        }catch (Exception e){
            return new ResponseEntity<>("fail to select book id", HttpStatus.BAD_REQUEST);
        }
    }

    // 즐겨찾기 등록 및 해제
    @PutMapping("/star/{id}")
    public ResponseEntity<?> updateStar(@RequestHeader(value = "access-token") String request, @PathVariable Long id){
        User user;
        try{
            String userEmail = jwtTokenProvider.getUserID(request);
            user = userService.myPage(userEmail);
            if (user == null) {
                throw new Exception();
            }
        }catch (Exception e){
            return new ResponseEntity<>("unauthorized Token", HttpStatus.UNAUTHORIZED);
        }

        try{
            if(starService.updateStar(user, id)){
                return new ResponseEntity<>(HttpStatus.OK);
            }else{
                throw new Exception();
            }
        }catch (Exception e){
            return new ResponseEntity<>("fail to add or delete star", HttpStatus.BAD_REQUEST);
        }
    }

    // 즐겨찾기 도서 목록 조회
    @GetMapping("/star")
    public ResponseEntity<?> getStar(@RequestHeader(value = "access-token") String request){
        User user;
        try{
            String userEmail = jwtTokenProvider.getUserID(request);
            user = userService.myPage(userEmail);
            if (user == null) {
                throw new Exception();
            }
        }catch (Exception e){
            return new ResponseEntity<>("unauthorized Token", HttpStatus.UNAUTHORIZED);
        }
        return new ResponseEntity<>(user.getStarList(), HttpStatus.OK);
    }

    // 읽은 기록 저장
    @PutMapping("/log/{id}")
    public ResponseEntity<?> addLog(@RequestHeader(value = "access-token") String request, @PathVariable Long id){
        User user;
        try{
            String userEmail = jwtTokenProvider.getUserID(request);
            user = userService.myPage(userEmail);
            if (user == null) {
                throw new Exception();
            }
        }catch (Exception e){
            return new ResponseEntity<>("unauthorized Token", HttpStatus.UNAUTHORIZED);
        }

        try{
            if(starService.updateLog(user, id)){
                return new ResponseEntity<>(HttpStatus.OK);
            }else{
                throw   new Exception();
            }
        }catch (Exception e){
            e.printStackTrace();
            return new ResponseEntity<>("fail to save log", HttpStatus.BAD_REQUEST);
        }
    }

    // 읽은 도서 목록 조회
    @GetMapping("/log")
    public ResponseEntity<?> getLog(@RequestHeader(value = "access-token") String request){
        User user;
        try{
            String userEmail = jwtTokenProvider.getUserID(request);
            user = userService.myPage(userEmail);
            if (user == null) {
                throw new Exception();
            }
        }catch (Exception e){
            return new ResponseEntity<>("unauthorized Token", HttpStatus.UNAUTHORIZED);
        }
        return new ResponseEntity<>(user.getLogList(), HttpStatus.OK);
    }


}
