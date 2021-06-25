package controller;

import javax.servlet.ServletException;
import javax.servlet.ServletOutputStream;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.List;

import Repository.Repository;
import domain.VacationDestination;
import org.json.JSONObject;

public class MainController extends HttpServlet {

    @Override
    protected void doPost(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        int id = Integer.parseInt(req.getParameter("destination"));
        String user = req.getParameter("user");
        new Repository().addBannedList(user, id);

    }

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String destination = req.getParameter("destination");
        String user = req.getParameter("user");
        List<VacationDestination> result = new Repository().getLikeDestination(destination, user);
        result.forEach(System.out::println);
        System.out.println(result.size());
        var obj = new JSONObject();
        obj.accumulate("data", result);
        PrintWriter out = resp.getWriter();
        out.println(obj);
    }
}
