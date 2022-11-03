package com.ssafy.b305.controller;

import com.ssafy.b305.domain.dto.BookDetailResponseDto;
import com.ssafy.b305.domain.dto.BookInfoResponseDto;
import com.ssafy.b305.domain.dto.BookRequestDto;
import com.ssafy.b305.service.BookService;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

import static org.junit.Assert.*;
@RunWith(SpringRunner.class)
@SpringBootTest //Springboot올려서 테스트
@Transactional  //Rollback 하기 위함
public class BookControllerTest {

    @Autowired
    BookService bookService;

    @Test
    public void getBook() throws Exception{
        // given
        Long id = new Long(1);

        // when
        BookDetailResponseDto result = bookService.getBook(id);

        // then
        assertEquals(result.getTitle(), "개구리 왕자");
    }

}