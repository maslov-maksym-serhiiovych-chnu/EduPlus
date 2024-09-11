package ua.edu.chnu.comments.repositories;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;
import ua.edu.chnu.comments.models.Comment;

@Repository
public interface CommentRepository extends CrudRepository<Comment, Integer> {
}
