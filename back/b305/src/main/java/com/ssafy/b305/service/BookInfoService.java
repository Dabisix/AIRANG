package com.ssafy.b305.service;

import com.ssafy.b305.domain.entity.Book;
import com.ssafy.b305.domain.entity.BookInfo;
import com.ssafy.b305.domain.entity.User;
import com.ssafy.b305.repository.BookRepository;
import com.ssafy.b305.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class BookInfoService {

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private BookRepository bookRepository;


    public boolean updateStar(User user, Long id) {
        try {
            List<BookInfo> starList = user.getStarList();

            boolean isDeleted = false;
            for(int i=0; i<starList.size(); i++){
                BookInfo book = starList.get(i);
                if(book.getBId()==id){
                    user.getStarList().remove(i);
                    isDeleted = true;
                    break;
                }
            }
            if (!isDeleted) {
                Book book = bookRepository.findBybId(id).get();
                user.getStarList().add(new BookInfo(id, book.getTitle()));
            }

            User result = userRepository.save(user);
            if(result.getU_id() == user.getU_id()) {
                return true;
            }else{
                throw new Exception();
            }
        }catch(Exception e){
            return false;
        }
    }

    public boolean updateLog(User user, Long id) {
        try{
            List<BookInfo> list = user.getLogList();
            for(BookInfo info : list){
                if (info.getBId() == id) {
                    user.getLogList().remove(id.intValue());
                    break;
                }
            }
            Book book = bookRepository.findBybId(id).get();
            user.getLogList().add(new BookInfo(id, book.getTitle()));
            User result = userRepository.save(user);
            if(result.getU_id() == user.getU_id()) {
                return true;
            }else{
                throw new Exception();
            }
        }catch(Exception e){
            return false;
        }
    }
}
