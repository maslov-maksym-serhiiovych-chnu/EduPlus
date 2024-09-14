create table if not exists comments
(
    id      serial primary key,
    author  varchar(255) not null,
    content text         not null
);