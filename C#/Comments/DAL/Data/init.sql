CREATE TABLE IF NOT EXISTS comments
(
    id      SERIAL PRIMARY KEY,
    author  VARCHAR(255) NOT NULL,
    content TEXT         NOT NULL
)