select
    p.Id, 
    t1.content as name_translation_key,
    t2.content as description_translation_key,
    p.price,
    p.max_order,
    avalaible,
    published,
    category_id,
    t3.content as full_translation_key
from products p
left outer join translations t1 on p.name_translation_key = t1.key
left outer join translations t2 on p.description_translation_key = t2.key
left outer join translations t3 on p.full_translation_key = t3.key
where 
t1.lang = t2.lang
and t2.lang = t3.lang
and t3.lang = 'hr'
and published = true


