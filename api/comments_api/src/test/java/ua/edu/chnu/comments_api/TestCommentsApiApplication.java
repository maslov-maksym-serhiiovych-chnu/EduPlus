package ua.edu.chnu.comments_api;

import org.springframework.boot.SpringApplication;

public class TestCommentsApiApplication {
	public static void main(String[] args) {
		SpringApplication.from(CommentsApiApplication::main).with(TestcontainersConfiguration.class).run(args);
	}
}