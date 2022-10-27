package com.ssafy.b305.repository;

import com.ssafy.b305.domain.entity.Auth;
import com.ssafy.b305.domain.entity.Book;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface BookRepository extends JpaRepository<Book, Integer> {

    Optional<Book> findByTitle(String title);
}
