package ua.edu.chnu.courses.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ua.edu.chnu.courses.models.Course;

@Repository
public interface CourseRepository extends JpaRepository<Course, Integer> {
}
