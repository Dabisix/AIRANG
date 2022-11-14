package com.ssafy.b305.controller;

import com.ssafy.b305.annotation.NoJwt;
import com.ssafy.b305.domain.dto.BookInfoResponseDto;
import com.ssafy.b305.domain.dto.BookRequestDto;
import com.ssafy.b305.domain.dto.BookDetailResponseDto;
import com.ssafy.b305.domain.dto.PageDto;
import com.ssafy.b305.domain.entity.BookInfo;
import com.ssafy.b305.domain.entity.User;
import com.ssafy.b305.jwt.JwtTokenProvider;
import com.ssafy.b305.service.BookService;
import com.ssafy.b305.service.BookInfoService;
import com.ssafy.b305.service.UserService;

import java.util.HashMap;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.ServletOutputStream;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.util.List;
import java.util.Random;

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
    BookInfoService bookInfoService;

    // 도서 상세 정보 조회
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

    // 도서 목록 조회
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
            PageDto pageDto = bookService.getBookList(bookRequestDto);
            if(pageDto.getBooklist()==null || pageDto.getBooklist().size()==0){
                return new ResponseEntity<>("no books that satisfing condition", HttpStatus.NO_CONTENT);
            }else{
                return new ResponseEntity<>(pageDto, HttpStatus.OK);
            }
        }catch (Exception e){
            return new ResponseEntity<>("fail to select book id or doesn't exist page number", HttpStatus.BAD_REQUEST);
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
            if(bookInfoService.updateStar(user, id)){
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

        HashMap<String, List<BookInfo>> ret = new HashMap<String, List<BookInfo>>();
        ret.put("booklist", user.getStarList());

        return new ResponseEntity<>(ret, HttpStatus.OK);
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
            if(bookInfoService.updateLog(user, id)){
                return new ResponseEntity<>(HttpStatus.OK);
            }else{
                throw new Exception();
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
        HashMap<String, List<BookInfo>> ret = new HashMap<String, List<BookInfo>>();
        ret.put("booklist", user.getLogList());

        return new ResponseEntity<>(ret, HttpStatus.OK);
    }

    @NoJwt
    @GetMapping("/narration")
    public void getNarration(HttpServletResponse response, @RequestHeader(value = "access-token") String request,
                             @RequestHeader String id, @RequestHeader String page, @RequestHeader String type) throws ServletException, IOException {
        // check user info
        User user;
        try {
            String userEmail = jwtTokenProvider.getUserID(request);
            user = userService.myPage(userEmail);
            if (user == null) {
                throw new Exception();
            }
        } catch (Exception e) {
            throw new ServletException(e.getMessage());
        }

        BookDetailResponseDto book = bookService.getBook(Long.parseLong(id));
        System.out.println("book ==== >>>> " + book.getTitle());

        File file;
        ServletOutputStream stream = null;
        BufferedInputStream buf = null;

        try {
            String root_path = "/home/ubuntu/";

            if (Integer.parseInt(type) == 0) {
                root_path += "naver/" + book.getTitle();
            } else if (Integer.parseInt(type) == 1) {
                root_path += "tts/voice/ai/" + user.getEmail() + "/" + id;
            }

            file = new File(root_path + "/" + page + ".mp3");

            // Content-Type
            response.setContentType("audio/mpeg");

            // Content-Disposition
            response.setHeader(HttpHeaders.CONTENT_DISPOSITION, "attachment;filename=" + file.getName());

            // Content-Length
            response.setContentLength((int) file.length());

            FileInputStream input = new FileInputStream(file);
            stream = response.getOutputStream();
            buf = new BufferedInputStream(input);

            // write to body
            int readBytes = 0;
            while ((readBytes = buf.read()) != -1)
                System.out.println("result => " + buf.read());
            stream.write(readBytes);
        } catch (IOException ioe) {
            throw new ServletException(ioe.getMessage());
        } finally {
            if (stream != null) stream.close();
            if (buf != null) buf.close();
        }
    }

    @GetMapping("/recommend")
    public ResponseEntity<?> getRecommendationList(@RequestHeader(value = "access-token") String request){
        // check user info
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

        // get recommended list
        HashMap<String, HashMap<String, Object>> ret = new HashMap<>();
        try{
            int size = user.getLogList().size();
            if(size > 0){
                BookInfoResponseDto logbook = new BookInfoResponseDto(user.getLogList().get(0));
                List<BookInfoResponseDto> recList = bookService.getRecList(logbook.getBId());
                HashMap<String, Object> value = new HashMap<>();
                value.put("targetBook", logbook);
                value.put("recList", recList);
                ret.put("logRec", value);
            }else{
                ret.put("logRec", null);
            }

            size = user.getStarList().size();
            if(size > 0){
                Random rand = new Random();
                BookInfoResponseDto starbook = new BookInfoResponseDto(user.getStarList().get(rand.nextInt(size)));
                List<BookInfoResponseDto> recList = bookService.getRecList(starbook.getBId());
                HashMap<String, Object> value = new HashMap<>();
                value.put("targetBook", starbook);
                value.put("recList", recList);
                ret.put("starRec", value);
            }else{
                ret.put("starRec", null);
            }

            return new ResponseEntity<>(ret, HttpStatus.OK);
        }catch (Exception e){
            return new ResponseEntity<>("fail to select recList", HttpStatus.BAD_REQUEST);
        }
    }

}
