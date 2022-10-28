package com.ssafy.b305.service;

import com.ssafy.b305.domain.entity.Book;
import com.ssafy.b305.repository.BookRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class BookService {

    @Autowired
    BookRepository bookRepository;

    public String getScripts(String title) {
        Book book = bookRepository.findByTitle(title).get();

        if(book != null)
            return book.getStory();
        return "";
    }
}
