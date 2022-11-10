package com.ssafy.b305.service;

import com.ssafy.b305.domain.dto.Mail;
import lombok.RequiredArgsConstructor;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.stereotype.Service;

import javax.transaction.Transactional;

@Service
@RequiredArgsConstructor
@Transactional
public class MailService {

    private final JavaMailSender mailSender;

    private static final String title = "[아이랑] 임시 비밀번호 안내 이메일입니다.";

    private static final String message = "안녕하세요. [아이랑] 임시 비밀번호 안내 메일입니다. "
            +"\n" + "회원님의 임시 비밀번호는 아래와 같습니다. 로그인 후 반드시 비밀번호를 변경해주세요."+"\n";
    private static final String fromAddress = "helpAiRang@gmail.com";

    public Mail createMail(String tmpPassword, String memberEmail) {
        Mail mail = Mail.builder()
                .toAddress(memberEmail)
                .title(title)
                .message(message + tmpPassword)
                .fromAddress(fromAddress)
                .build();

        return mail;
    }

    public void sendMail(Mail mail) {
        SimpleMailMessage mailMessage = new SimpleMailMessage();
        mailMessage.setTo(mail.getToAddress());
        mailMessage.setSubject(mail.getTitle());
        mailMessage.setText(mail.getMessage());
        mailMessage.setFrom(mail.getFromAddress());
        mailMessage.setReplyTo(mail.getFromAddress());

        mailSender.send(mailMessage);
    }
}

