package ua.edu.chnu.comments.models;

import jakarta.persistence.*;
import lombok.EqualsAndHashCode;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@NoArgsConstructor
@EqualsAndHashCode
@Entity
@Table(name = "comments")
public class Comment {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    @Getter
    @Setter
    @Column(nullable = false)
    private String author;

    @Getter
    @Setter
    @Column(nullable = false)
    private String content;

    public Comment(String author, String content) {
        this.author = author;
        this.content = content;
    }
}