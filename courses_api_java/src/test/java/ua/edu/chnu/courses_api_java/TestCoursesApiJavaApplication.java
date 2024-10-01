package ua.edu.chnu.courses_api_java;

import org.springframework.boot.SpringApplication;

public class TestCoursesApiJavaApplication {

	public static void main(String[] args) {
		SpringApplication.from(CoursesApiJavaApplication::main).with(TestcontainersConfiguration.class).run(args);
	}

}
