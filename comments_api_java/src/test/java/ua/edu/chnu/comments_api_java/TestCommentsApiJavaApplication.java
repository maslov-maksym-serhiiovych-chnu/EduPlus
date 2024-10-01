package ua.edu.chnu.comments_api_java;

import org.springframework.boot.SpringApplication;

public class TestCommentsApiJavaApplication {

	public static void main(String[] args) {
		SpringApplication.from(CommentsApiJavaApplication::main).with(TestcontainersConfiguration.class).run(args);
	}

}
