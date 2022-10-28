package com.ssafy.b305.domain.dto;

import com.ssafy.b305.domain.entity.Book;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class BookDetailResponseDto {
    private Long bId;
    private String title;
    private String author;
    private String kContent;
    private String eContent;
    private int cnt;
    private boolean aFlag;

    public BookDetailResponseDto(Book book){
        this.bId = book.getBId();
        this.title = book.getTitle();
        this.kContent = book.getKContent();
        this.eContent = book.getEContent();
        this.cnt = book.getCnt();
        this.aFlag = book.isAFlag();
    }

}
