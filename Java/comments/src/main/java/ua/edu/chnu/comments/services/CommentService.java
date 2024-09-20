package ua.edu.chnu.comments.services;

import lombok.AllArgsConstructor;
import org.springframework.stereotype.Service;
import ua.edu.chnu.comments.dtos.CommentDTO;
import ua.edu.chnu.comments.exceptions.CommentNotFoundByIdException;
import ua.edu.chnu.comments.models.Comment;
import ua.edu.chnu.comments.repositories.CommentRepository;

import java.util.List;

@AllArgsConstructor
@Service
public class CommentService {
    private final CommentRepository repository;

    public CommentDTO create(CommentDTO commentDTO) {
        Comment comment = toModel(commentDTO);
        repository.save(comment);

        return commentDTO;
    }

    public List<CommentDTO> getAll() {
        return repository.findAll()
                .stream()
                .map(this::toDTO)
                .toList();
    }

    public CommentDTO get(int id) {
        checkExistence(id);

        Comment comment = repository.findById(id).orElse(null);
        return toDTO(comment);
    }

    public void update(int id, CommentDTO commentDTO) {
        checkExistence(id);

        Comment comment = toModel(commentDTO);
        comment.setId(id);

        repository.save(comment);
    }

    public void delete(int id) {
        checkExistence(id);

        repository.deleteById(id);
    }

    private void checkExistence(int id) {
        if (!repository.existsById(id)) {
            throw new CommentNotFoundByIdException(id);
        }
    }

    private Comment toModel(CommentDTO commentDTO) {
        if (commentDTO == null) {
            return null;
        }

        Comment comment = new Comment();
        comment.setAuthor(commentDTO.getAuthor());
        comment.setContent(commentDTO.getContent());

        return comment;
    }

    private CommentDTO toDTO(Comment comment) {
        if (comment == null) {
            return null;
        }

        return new CommentDTO(comment.getAuthor(), comment.getContent());
    }
}
