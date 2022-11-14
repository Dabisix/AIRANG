package com.ssafy.b305.repository;

import com.ssafy.b305.domain.entity.Book;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface BookRepository extends JpaRepository<Book, Integer> {
    Optional<Book> findBybId(Long id);


    @Query(value = "SELECT * FROM book WHERE title LIKE %:keyword% AND a_flag=:aFlag", nativeQuery = true)
    List<Book> findBookList(@Param("keyword") String keyword, @Param("aFlag") int aFlag);
}
