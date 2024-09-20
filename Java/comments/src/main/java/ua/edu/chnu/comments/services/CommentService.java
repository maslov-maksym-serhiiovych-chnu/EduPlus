package ua.edu.chnu.comments.services;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import ua.edu.chnu.comments.dtos.CommentDTO;
import ua.edu.chnu.comments.exceptions.CommentNotFoundByIdException;
import ua.edu.chnu.comments.models.Comment;
import ua.edu.chnu.comments.repositories.CommentRepository;

import java.util.List;

@Service
@RequiredArgsConstructor
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
        Comment comment = repository.findById(id).orElseThrow(() -> new CommentNotFoundByIdException(id));
        return toDTO(comment);
    }

    public void update(int id, CommentDTO commentDTO) {
        CommentDTO existing = get(id);
        existing.setAuthor(commentDTO.getAuthor());
        existing.setContent(commentDTO.getContent());

        Comment comment = toModel(existing);
        comment.setId(id);
        
        repository.save(comment);
    }

    public void delete(int id) {
        CommentDTO commentDTO = get(id);

        Comment comment = toModel(commentDTO);
        comment.setId(id);
        
        repository.delete(comment);
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

        CommentDTO commentDTO = new CommentDTO();
        commentDTO.setAuthor(comment.getAuthor());
        commentDTO.setContent(comment.getContent());

        return commentDTO;
    }
}
