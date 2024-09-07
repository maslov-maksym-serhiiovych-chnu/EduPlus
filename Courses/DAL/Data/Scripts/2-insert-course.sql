create or replace procedure insert_course(p_name varchar, p_description text)
    language sql
as
$$
insert into courses (name, description)
values (p_name, p_description)
$$