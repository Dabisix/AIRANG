package com.ssafy.b305.domain.entity;

import lombok.Getter;
import lombok.NoArgsConstructor;

import javax.persistence.Embeddable;
import java.sql.Timestamp;

@Getter
@NoArgsConstructor
@Embeddable
public class BookInfo {
    private Long bId;
    private String title;
    private Timestamp date;

    public BookInfo(Long bId, String title){
        this.bId = bId;
        this.title = title;
        this.date = new Timestamp(System.currentTimeMillis());
    }
}
