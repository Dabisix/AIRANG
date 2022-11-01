package com.ssafy.b305.repository;

import com.ssafy.b305.domain.entity.Auth;
import com.ssafy.b305.domain.entity.Book;
import com.ssafy.b305.domain.entity.BookInfo;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface BookRepository extends JpaRepository<Book, Integer> {

    Optional<Book> findBybId(Long id);

    @Query(value = "SELECT * FROM BOOK " +
            "WHERE TITLE LIKE '%:keyword%' AND A_FLAG = :aFlag ", nativeQuery = true)
    List<Book> findBookList(@Param("aFlag") int aFlag, @Param("keyword") String keyword);

}
