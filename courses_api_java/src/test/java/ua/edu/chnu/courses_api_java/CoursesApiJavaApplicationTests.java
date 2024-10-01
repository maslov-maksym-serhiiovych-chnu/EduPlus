package ua.edu.chnu.courses_api_java;

import org.junit.jupiter.api.Test;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.context.annotation.Import;

@Import(TestcontainersConfiguration.class)
@SpringBootTest
class CoursesApiJavaApplicationTests {

	@Test
	void contextLoads() {
	}

}
