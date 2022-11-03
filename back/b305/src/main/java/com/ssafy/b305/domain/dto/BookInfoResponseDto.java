package com.ssafy.b305.domain.dto;

import com.ssafy.b305.domain.entity.Book;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class BookInfoResponseDto {
    private Long bId;
    private String title;
    private boolean aFlag;

    public BookInfoResponseDto(Book book){
        this.bId = book.getBId();
        this.title = book.getTitle();
        this.aFlag = book.isAFlag();
    }
}
