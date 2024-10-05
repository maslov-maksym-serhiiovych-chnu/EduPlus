package ua.edu.chnu.comments_api.comments;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

@AllArgsConstructor
@NoArgsConstructor
@Data
@Document("comments")
public class Comment {
    @Id
    private String id;
    
    private String content;
    private int courseId;
}