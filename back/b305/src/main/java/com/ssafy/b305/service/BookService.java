package com.ssafy.b305.service;

import com.ssafy.b305.domain.dto.BookDetailResponseDto;
import com.ssafy.b305.domain.dto.BookInfoResponseDto;
import com.ssafy.b305.domain.dto.BookRequestDto;
import com.ssafy.b305.domain.entity.Book;
import com.ssafy.b305.domain.entity.BookInfo;
import com.ssafy.b305.repository.BookRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import java.util.*;
import java.util.stream.Collectors;

@Service
public class BookService {

    @Autowired
    BookRepository bookRepository;

    public BookDetailResponseDto getBook(Long id) {
        try{
            Optional<Book> book = bookRepository.findBybId(id);
            if(book.isPresent()){
                return new BookDetailResponseDto(book.get());
            }else{
                return null;
            }
        }catch (Exception e){
            return null;
        }
    }

    public List<BookInfoResponseDto> getBookList(BookRequestDto bookRequestDto) {
        try{
            int flag = bookRequestDto.getAFlag();
            String keyword = (bookRequestDto.getKeyword()==null ? "" : bookRequestDto.getKeyword());
            List<Book> bookList = new ArrayList<Book>();
            System.out.println("flag in BookService = " + flag);
            System.out.println("keyword in BookService = " + keyword);
            if(flag == 0){ // 모두
                for(int i=0; i<2; i++){
                    bookList.addAll(bookRepository.findBookList(keyword, i));
                }
            }else{
                if(flag == 2) { // ai 미지원
                    flag = 0;
                }
                bookList = bookRepository.findBookList(keyword, flag);
            }

            // 정렬
            if(bookRequestDto.getSort()==1) { // 제목 오름차순 정렬
                bookList = bookList.stream().sorted(Comparator.comparing(Book::getTitle)).collect(Collectors.toList());
            }else if(bookRequestDto.getSort()==2){ // 조회수 내림차순 정렬
                bookList = bookList.stream().sorted(Comparator.comparing(Book::getCnt)).collect(Collectors.toList());
                Collections.reverse(bookList);
            }

            List<BookInfoResponseDto> responseList = new ArrayList<BookInfoResponseDto>();
            for(Book book : bookList){
                responseList.add(new BookInfoResponseDto(book));
            }
            return responseList;

        }catch (Exception e){
            return null;
        }
    }
}
