package com.ssafy.b305.domain.entity;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Getter;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Getter
@NoArgsConstructor
@AllArgsConstructor
@Builder
@Entity
public class Book {

    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Id
    private Long b_id;

    @Column
    private String title;

    @Column
    private String author;

    @Column(columnDefinition = "MEDIUMTEXT")
    private String k_content;

    @Column(columnDefinition = "MEDIUMTEXT")
    private String e_content;

    @Column
    private int cnt;

    @Column
    private boolean a_flag;

}
