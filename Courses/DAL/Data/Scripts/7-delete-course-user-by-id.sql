create or replace procedure delete_course_user_by_id(p_id integer)
    language sql
as
$$
delete
from course_users
where id = p_id
$$