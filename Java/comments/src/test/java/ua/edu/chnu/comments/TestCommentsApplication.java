package ua.edu.chnu.comments;

import org.springframework.boot.SpringApplication;

public class TestCommentsApplication {

	public static void main(String[] args) {
		SpringApplication.from(CommentsApplication::main).with(TestcontainersConfiguration.class).run(args);
	}

}
