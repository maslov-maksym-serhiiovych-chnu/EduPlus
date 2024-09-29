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
        var commentDTOs = service.readAll()
                .stream()
                .map(CommentController::toDTO)
                .toList();
        return ResponseEntity.ok(commentDTOs);
    }

    @GetMapping("{id}")
    public ResponseEntity<CommentDTO> get(@PathVariable int id) {
        Comment comment = service.read(id);

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

        service.update(id, comment);

        return ResponseEntity.noContent().build();
    }

    @DeleteMapping("{id}")
    public ResponseEntity<CommentDTO> delete(@PathVariable int id) {
        service.delete(id);

        return ResponseEntity.noContent().build();
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
