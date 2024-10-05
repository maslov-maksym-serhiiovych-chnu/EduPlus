package ua.edu.chnu.courses_api;

import org.springframework.boot.SpringApplication;

public class TestCoursesApiApplication {
    public static void main(String[] args) {
        SpringApplication.from(CoursesApiApplication::main).with(TestcontainersConfiguration.class).run(args);
    }
}