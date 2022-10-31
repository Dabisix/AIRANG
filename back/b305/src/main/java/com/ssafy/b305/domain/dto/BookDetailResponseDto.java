package com.ssafy.b305.domain.dto;

import com.ssafy.b305.domain.entity.Book;
import lombok.Getter;
import lombok.Setter;

import java.util.ArrayList;
import java.util.List;

@Getter
@Setter
public class BookDetailResponseDto {
    private Long bId;
    private String title;
    private String author;
    private String[] kContent;
    private String[] eContent;
    private int cnt;
    private boolean aFlag;

    public BookDetailResponseDto(Book book){
        this.bId = book.getBId();
        this.title = book.getTitle();
        this.cnt = book.getCnt();
        this.aFlag = book.isAFlag();

        // 페이지별로 내용 나눠 저장
        List<String> content = new ArrayList<String>();
        String[] request_contents = book.getKContent().split("&page&");
        for (String s : request_contents){
            content.add(s);
        }
        this.kContent = content.toArray(new String[content.size()]);

        content = new ArrayList<String>();
        request_contents = book.getEContent().split("&page&");
        for (String s : request_contents){
            content.add(s);
        }
        this.eContent = content.toArray(new String[content.size()]);
    }

}
