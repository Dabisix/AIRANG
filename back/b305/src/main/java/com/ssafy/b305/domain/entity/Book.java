package com.ssafy.b305.domain.entity;

import lombok.*;

import javax.persistence.*;

@Getter
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@Entity(name="book")
public class Book {

    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Id
    @Column(name ="b_id")
    private Long bId;

    @Column
    private String title;

    @Column
    private String author;

    @Column(name = "k_content", columnDefinition = "MEDIUMTEXT")
    private String kContent;

    @Column(name = "e_content", columnDefinition = "MEDIUMTEXT")
    private String eContent;

    @Column
    private int cnt;

    @Column
    private boolean aFlag;

    @Builder
    public Book(Long bId, String title, String author, String kContent, String eContent, int cnt, boolean aFlag){
        this.bId = bId;
        this.title = title;
        this.author = author;
        this.kContent = kContent;
        this.eContent = eContent;
        this.cnt = cnt;
        this.aFlag = aFlag;
    }

    public void addCnt(){
        this.cnt++;
    }

}
