package controller;

import javax.servlet.*;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

public class CORS implements Filter {
    public CORS() {
    }

    public void destroy() {
    }
    public void doFilter(ServletRequest servletRequest, ServletResponse servletResponse, FilterChain chain)
            throws IOException, ServletException {

        HttpServletRequest request = (HttpServletRequest) servletRequest;
        System.out.println("CORSFilter HTTP Request: " + request.getMethod());
        ((HttpServletResponse) servletResponse).addHeader("Access-Control-Allow-Origin", "*");
        ((HttpServletResponse) servletResponse).addHeader("Access-Control-Allow-Headers", "*");
        ((HttpServletResponse) servletResponse).addHeader("Access-Control-Allow-Methods", "*");
        HttpServletResponse resp = (HttpServletResponse) servletResponse;
        // For HTTP OPTIONS verb/method reply with ACCEPTED status code -- per controller.CORS handshake
        if (request.getMethod().equals("OPTIONS")) {
            resp.setStatus(HttpServletResponse.SC_ACCEPTED);
            return;
        }
        // pass the request along the filter chain
        chain.doFilter(request, servletResponse);
    }

    public void init(FilterConfig fConfig) {
    }
}
