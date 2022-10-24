package com.ssafy.b305.domain.entity;

import lombok.*;
import org.hibernate.annotations.DynamicInsert;

import javax.persistence.*;

@Getter
@Setter
@Builder
@AllArgsConstructor
@NoArgsConstructor
@DynamicInsert
@Entity
public class User {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long u_id;

    @Column(unique = true)
    private String email;

    @Column
    private String pw;

    @Column
    private String name;
}
