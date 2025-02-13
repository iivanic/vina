PGDMP      &                }            vina-ivanic    14.6    16.0 +    #           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            $           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            %           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            &           1262    73682    vina-ivanic    DATABASE     �   CREATE DATABASE "vina-ivanic" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Croatian_Croatia.1252';
    DROP DATABASE "vina-ivanic";
                quizquest_user    false                        2615    2200    public    SCHEMA     2   -- *not* creating schema, since initdb creates it
 2   -- *not* dropping schema, since initdb creates it
                postgres    false            '           0    0    SCHEMA public    ACL     Q   REVOKE USAGE ON SCHEMA public FROM PUBLIC;
GRANT ALL ON SCHEMA public TO PUBLIC;
                   postgres    false    4            �            1259    73693 
   categories    TABLE     d   CREATE TABLE public.categories (
    id integer NOT NULL,
    name_translation_key text NOT NULL
);
    DROP TABLE public.categories;
       public         heap    quizquest_user    false    4            �            1259    73692    categories_id_seq    SEQUENCE     �   ALTER TABLE public.categories ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.categories_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          quizquest_user    false    212    4            �            1259    73684 	   customers    TABLE     �   CREATE TABLE public.customers (
    id integer NOT NULL,
    email text NOT NULL,
    is_admin boolean DEFAULT false NOT NULL
);
    DROP TABLE public.customers;
       public         heap    pg_database_owner    false    4            �            1259    73683    customers_id_seq    SEQUENCE     �   ALTER TABLE public.customers ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.customers_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          pg_database_owner    false    4    210            �            1259    73701    products    TABLE     h  CREATE TABLE public.products (
    id integer NOT NULL,
    name_translation_key text NOT NULL,
    description_translation_key text,
    price double precision DEFAULT 0 NOT NULL,
    max_order integer DEFAULT 6 NOT NULL,
    avalaible boolean DEFAULT true NOT NULL,
    published boolean DEFAULT false NOT NULL,
    category_id integer DEFAULT 1 NOT NULL
);
    DROP TABLE public.products;
       public         heap    quizquest_user    false    4            �            1259    73700    products_id_seq    SEQUENCE     �   ALTER TABLE public.products ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.products_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          quizquest_user    false    214    4            �            1259    73726    tokens    TABLE     �   CREATE TABLE public.tokens (
    id integer NOT NULL,
    email text NOT NULL,
    valid_from date NOT NULL,
    valid_until date NOT NULL,
    token text NOT NULL
);
    DROP TABLE public.tokens;
       public         heap    quizquest_user    false    4            �            1259    73725    tokens_id_seq    SEQUENCE     �   ALTER TABLE public.tokens ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.tokens_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          quizquest_user    false    218    4            �            1259    73718    translations    TABLE     �   CREATE TABLE public.translations (
    id integer NOT NULL,
    key text NOT NULL,
    content text NOT NULL,
    lang text NOT NULL
);
     DROP TABLE public.translations;
       public         heap    quizquest_user    false    4            �            1259    73717    translations_id_seq    SEQUENCE     �   ALTER TABLE public.translations ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.translations_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          quizquest_user    false    216    4                      0    73693 
   categories 
   TABLE DATA           >   COPY public.categories (id, name_translation_key) FROM stdin;
    public          quizquest_user    false    212   n/                 0    73684 	   customers 
   TABLE DATA           8   COPY public.customers (id, email, is_admin) FROM stdin;
    public          pg_database_owner    false    210   �/                 0    73701    products 
   TABLE DATA           �   COPY public.products (id, name_translation_key, description_translation_key, price, max_order, avalaible, published, category_id) FROM stdin;
    public          quizquest_user    false    214   �/                  0    73726    tokens 
   TABLE DATA           K   COPY public.tokens (id, email, valid_from, valid_until, token) FROM stdin;
    public          quizquest_user    false    218   '0                 0    73718    translations 
   TABLE DATA           >   COPY public.translations (id, key, content, lang) FROM stdin;
    public          quizquest_user    false    216   D0       (           0    0    categories_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.categories_id_seq', 2, true);
          public          quizquest_user    false    211            )           0    0    customers_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.customers_id_seq', 1, true);
          public          pg_database_owner    false    209            *           0    0    products_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.products_id_seq', 3, true);
          public          quizquest_user    false    213            +           0    0    tokens_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.tokens_id_seq', 1, false);
          public          quizquest_user    false    217            ,           0    0    translations_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.translations_id_seq', 15, true);
          public          quizquest_user    false    215            {           2606    73699    categories categories_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.categories
    ADD CONSTRAINT categories_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.categories DROP CONSTRAINT categories_pkey;
       public            quizquest_user    false    212            w           2606    73691    customers customers_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_pkey;
       public            pg_database_owner    false    210            �           2606    73712    products products_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.products DROP CONSTRAINT products_pkey;
       public            quizquest_user    false    214            �           2606    73732    tokens tokens_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.tokens
    ADD CONSTRAINT tokens_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.tokens DROP CONSTRAINT tokens_pkey;
       public            quizquest_user    false    218            �           2606    73724    translations translations_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.translations
    ADD CONSTRAINT translations_pkey PRIMARY KEY (id);
 H   ALTER TABLE ONLY public.translations DROP CONSTRAINT translations_pkey;
       public            quizquest_user    false    216            |           1259    73734 	   ix_cat_id    INDEX     ^   CREATE INDEX ix_cat_id ON public.categories USING btree (id) WITH (deduplicate_items='true');
    DROP INDEX public.ix_cat_id;
       public            quizquest_user    false    212            x           1259    73738 
   ix_cust_id    INDEX     ^   CREATE INDEX ix_cust_id ON public.customers USING btree (id) WITH (deduplicate_items='true');
    DROP INDEX public.ix_cust_id;
       public            pg_database_owner    false    210            y           1259    73735    ix_email    INDEX     _   CREATE INDEX ix_email ON public.customers USING btree (email) WITH (deduplicate_items='true');
    DROP INDEX public.ix_email;
       public            pg_database_owner    false    210            }           1259    73741    ix_prod_avail    INDEX     g   CREATE INDEX ix_prod_avail ON public.products USING btree (avalaible) WITH (deduplicate_items='true');
 !   DROP INDEX public.ix_prod_avail;
       public            quizquest_user    false    214            ~           1259    73737 
   ix_prod_id    INDEX     ]   CREATE INDEX ix_prod_id ON public.products USING btree (id) WITH (deduplicate_items='true');
    DROP INDEX public.ix_prod_id;
       public            quizquest_user    false    214                       1259    73742    ix_prod_published    INDEX     k   CREATE INDEX ix_prod_published ON public.products USING btree (published) WITH (deduplicate_items='true');
 %   DROP INDEX public.ix_prod_published;
       public            quizquest_user    false    214            �           1259    73745    ix_token_email    INDEX     b   CREATE INDEX ix_token_email ON public.tokens USING btree (email) WITH (deduplicate_items='true');
 "   DROP INDEX public.ix_token_email;
       public            quizquest_user    false    218            �           1259    73744    ix_token_token    INDEX     b   CREATE INDEX ix_token_token ON public.tokens USING btree (token) WITH (deduplicate_items='true');
 "   DROP INDEX public.ix_token_token;
       public            quizquest_user    false    218            �           1259    73746    ix_token_valid    INDEX     h   CREATE INDEX ix_token_valid ON public.tokens USING btree (valid_until) WITH (deduplicate_items='true');
 "   DROP INDEX public.ix_token_valid;
       public            quizquest_user    false    218            �           1259    73747    ix_trans_key    INDEX     d   CREATE INDEX ix_trans_key ON public.translations USING btree (key) WITH (deduplicate_items='true');
     DROP INDEX public.ix_trans_key;
       public            quizquest_user    false    216            �           1259    73748    ix_trans_lang    INDEX     f   CREATE INDEX ix_trans_lang ON public.translations USING btree (lang) WITH (deduplicate_items='true');
 !   DROP INDEX public.ix_trans_lang;
       public            quizquest_user    false    216            �           2606    73749    products fk_prod_cat    FK CONSTRAINT     �   ALTER TABLE ONLY public.products
    ADD CONSTRAINT fk_prod_cat FOREIGN KEY (category_id) REFERENCES public.categories(id) NOT VALID;
 >   ALTER TABLE ONLY public.products DROP CONSTRAINT fk_prod_cat;
       public          quizquest_user    false    212    214    3195               -   x�3�,(��Ɍ�N�M͋ON,�2�L)��J�/*�J��qqq �'         %   x�3���K�w(��K��,K��L��(�,����� �:�         7   x�3�,Ȏ72���)��ɜ��f�%@h�e̙R��h�F\F`]PCo� ���             x������ � �         �   x�}�K�0���)z����ʘ4�N"�@
H���y/;�1<����7�V��-Syr�B�I������b��<ؚ$�Q��)dX��C#��~dWT�}��;�´�Ҍx̏])LYcM�嘞:���b'_�*W�3�l��מV�R+��uZ+�1���p��9�K�iCS{7�����@�Ï����a�T     