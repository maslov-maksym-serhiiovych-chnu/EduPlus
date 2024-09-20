create table comments
(
    id      serial primary key,
    author  varchar(255) not null,
    content text         not null
)