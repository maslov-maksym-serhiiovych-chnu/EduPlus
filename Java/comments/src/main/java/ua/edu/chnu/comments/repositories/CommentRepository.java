package ua.edu.chnu.comments.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ua.edu.chnu.comments.models.Comment;

@Repository
public interface CommentRepository extends JpaRepository<Comment, Integer> {
}
