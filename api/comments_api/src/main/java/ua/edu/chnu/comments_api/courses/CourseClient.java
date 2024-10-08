package ua.edu.chnu.comments_api.courses;

import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;

@FeignClient(name = "courses-api", url = "http://localhost:8081/api/courses")
public interface CourseClient {
    @GetMapping("{id}")
    ResponseEntity<Course> read(@PathVariable int id);
}