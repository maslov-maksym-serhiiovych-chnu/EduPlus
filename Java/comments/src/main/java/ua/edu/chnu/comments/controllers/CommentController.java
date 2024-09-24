package ua.edu.chnu.comments.controllers;

import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ua.edu.chnu.comments.dtos.CommentDTO;
import ua.edu.chnu.comments.models.Comment;
import ua.edu.chnu.comments.services.CommentService;

import java.util.List;

@RequiredArgsConstructor
@RestController
@RequestMapping("/api/comments")
public class CommentController {
    private final CommentService service;

    @GetMapping
    public ResponseEntity<List<CommentDTO>> getAll() {
        var commentDTOS = service.getAll()
                .stream()
                .map(CommentController::toDTO)
                .toList();
        return ResponseEntity.ok(commentDTOS);
    }

    @GetMapping("{id}")
    public ResponseEntity<CommentDTO> get(@PathVariable int id) {
        Comment comment = service.get(id);

        CommentDTO commentDTO = toDTO(comment);
        return ResponseEntity.ok(commentDTO);
    }

    @PostMapping
    public ResponseEntity<CommentDTO> create(@RequestBody CommentDTO commentDTO) {
        Comment comment = toModel(commentDTO);

        Comment created = service.create(comment);

        CommentDTO createdDTO = toDTO(created);
        return ResponseEntity.status(HttpStatus.CREATED).body(createdDTO);
    }

    @PutMapping("{id}")
    public ResponseEntity<CommentDTO> update(@PathVariable int id, @RequestBody CommentDTO commentDTO) {
        Comment comment = toModel(commentDTO);

        Comment updated = service.update(id, comment);

        CommentDTO updatedDTO = toDTO(updated);
        return ResponseEntity.ok(updatedDTO);
    }

    @DeleteMapping("{id}")
    public ResponseEntity<CommentDTO> delete(@PathVariable int id) {
        Comment deleted = service.delete(id);

        CommentDTO deletedDTO = toDTO(deleted);
        return ResponseEntity.ok(deletedDTO);
    }

    public static Comment toModel(CommentDTO commentDTO) {
        if (commentDTO == null) {
            return null;
        }

        Comment comment = new Comment();
        comment.setAuthor(commentDTO.getAuthor());
        comment.setContent(commentDTO.getContent());

        return comment;
    }

    public static CommentDTO toDTO(Comment comment) {
        if (comment == null) {
            return null;
        }

        CommentDTO commentDTO = new CommentDTO();
        commentDTO.setAuthor(comment.getAuthor());
        commentDTO.setContent(comment.getContent());

        return commentDTO;
    }
}
