package com.ssafy.b305.service;

import com.ssafy.b305.domain.entity.Book;
import com.ssafy.b305.domain.entity.BookInfo;
import com.ssafy.b305.domain.entity.User;
import com.ssafy.b305.repository.BookRepository;
import com.ssafy.b305.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.*;

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
                user.getStarList().add(new BookInfo(id, book.getTitle(), book.isAFlag()));
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
            List<BookInfo> loglist = user.getLogList();
            for(int i=0; i<loglist.size(); i++){
                BookInfo book = loglist.get(i);
                if(book.getBId()==id){
                    user.getLogList().remove(i);
                    break;
                }
            }

            // 읽은 목록 추가
            Book book = bookRepository.findBybId(id).get();
            user.getLogList().add(new BookInfo(id, book.getTitle(), book.isAFlag()));
            Collections.sort(user.getLogList());

            // 읽은 책의 조회수 증가
            User result = userRepository.save(user);
            book.addCnt();
            bookRepository.save(book);

            if(result.getU_id() == user.getU_id()) {
                return true;
            }else{
                throw new Exception();
            }
        }catch(Exception e){
            return false;
        }
//        }
//            return false;
    }

    // 읽은 목록 또는 즐겨찾기 목록 중복 확인
    public void checkDuplicate(User user, boolean type){
        if(type==true){ //즐겨찾기 목록
            List<BookInfo> result = new ArrayList<BookInfo>();
            List<Long> idList = new ArrayList<Long>();
            for(BookInfo book : user.getStarList()){
                if(!idList.contains(book.getBId())) {
                    idList.add(book.getBId());
                    result.add(book);
                }
            }
            user.setStarList(result);
        }else{ // 읽은 목록
            List<BookInfo> result = new ArrayList<BookInfo>();
            List<Long> idList = new ArrayList<Long>();
            for(BookInfo book : user.getLogList()){
                if(!idList.contains(book.getBId())) {
                    idList.add(book.getBId());
                    result.add(book);
                }
            }
            user.setLogList(result);
        }
        userRepository.save(user);
    }
}
