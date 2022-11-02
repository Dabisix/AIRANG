package com.ssafy.b305.domain.dto;

import com.ssafy.b305.domain.entity.Book;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class BookInfoResponseDto {
    private Long bId;
    private String title;

    public BookInfoResponseDto(Book book){
        this.bId = book.getBId();
        this.title = book.getTitle();
    }
}
