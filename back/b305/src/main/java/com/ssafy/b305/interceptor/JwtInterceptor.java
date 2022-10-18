package com.ssafy.b305.interceptor;

import com.ssafy.b305.annotation.NoJwt;
import com.ssafy.b305.jwt.JwtTokenProvider;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.method.HandlerMethod;
import org.springframework.web.servlet.HandlerInterceptor;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class JwtInterceptor implements HandlerInterceptor {

    @Autowired
    JwtTokenProvider jwtService;

    @Override
    public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler)
            throws Exception {
        if(request.getMethod().equals("OPTIONS")) {
            return true;
        }

        boolean check = checkAnnotation(handler, NoJwt.class);
        if(check) return true;

        String accessToken = request.getHeader("access-token");

        if (accessToken != null && jwtService.isValidAccessToken(accessToken)) {
            return true;
        }
        response.setStatus(401);
        return false;
    }

    private boolean checkAnnotation(Object handler, Class cls){
        HandlerMethod handlerMethod = (HandlerMethod) handler;
        //해당 어노테이션이 존재하면 true.
        if(handlerMethod.getMethodAnnotation(cls) != null){
            return true;
        }
        return false;
    }
}
