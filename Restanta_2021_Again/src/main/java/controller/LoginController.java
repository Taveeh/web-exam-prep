package controller;

import org.json.JSONObject;
import org.json.JSONWriter;

import javax.servlet.ServletException;
import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class LoginController extends HttpServlet {
    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        resp.setContentType("application/json");

        String username = req.getParameter("username");
        PrintWriter out = resp.getWriter();
        if (username.isEmpty()) {
            req.getSession().setAttribute("error", "Username must not be empty");
//            req.getRequestDispatcher("login.jsp").include(req, resp);
            var obj = new JSONObject();
            obj.accumulate("type", "not success");
            out.println(obj);

            return;
        }
//        else if (password.isEmpty()) {
//            req.getSession().setAttribute("error", "Password must not be empty");
//            req.getRequestDispatcher("login.jsp").include(req, resp);
//        }
        resp.addCookie(new Cookie("user", username));
        var obj = new JSONObject();
        obj.accumulate("type", "success");
        out.println(obj);
//        resp.sendRedirect("main");
    }

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        resp.setContentType("text/html");
        resp.setHeader("Access-Control-Allow-Origin", "*");
        PrintWriter out = resp.getWriter();
//        out.println(new JSONObject("{\"type\": \"success\"}").toString());
        var obj = new JSONObject();
        obj.accumulate("type", "success2");
        out.println(obj);

    }
}
