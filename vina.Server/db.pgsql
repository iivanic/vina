
--
-- Name: categories; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.categories (
    id integer NOT NULL,
    name_translation_key text NOT NULL
);

--
-- Name: categories_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.categories ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.categories_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: countries; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.countries (
    id integer NOT NULL,
    name text
);


--
-- Name: countries_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.countries ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.countries_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: customers; Type: TABLE; Schema: public; Owner: pg_database_owner
--

CREATE TABLE public.customers (
    id integer NOT NULL,
    email text NOT NULL,
    is_admin boolean DEFAULT false NOT NULL
);



--
-- Name: customers_id_seq; Type: SEQUENCE; Schema: public; Owner: pg_database_owner
--

ALTER TABLE public.customers ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.customers_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: orders; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.orders (
    id integer NOT NULL,
    order_time timestamp with time zone NOT NULL,
    payment_recieved timestamp with time zone,
    package_sent timestamp with time zone,
    package_recieveded timestamp with time zone,
    closed boolean,
    amount double precision NOT NULL,
    phone_for_delivery text,
    country text,
    state text,
    city text,
    address1 text,
    address2 text,
    comment text
);



--
-- Name: orders_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.orders ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.orders_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: products; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.products (
    id integer NOT NULL,
    name_translation_key text NOT NULL,
    description_translation_key text,
    price double precision DEFAULT 0 NOT NULL,
    max_order integer DEFAULT 6 NOT NULL,
    avalaible boolean DEFAULT true NOT NULL,
    published boolean DEFAULT false NOT NULL,
    category_id integer DEFAULT 1 NOT NULL,
    full_translation_key text
);



--
-- Name: products_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.products ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.products_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: tokens; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tokens (
    id integer NOT NULL,
    email text NOT NULL,
    valid_from date NOT NULL,
    valid_until date NOT NULL,
    token text NOT NULL
);



--
-- Name: tokens_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.tokens ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.tokens_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: translations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.translations (
    id integer NOT NULL,
    key text NOT NULL,
    content text NOT NULL,
    lang text NOT NULL
);



--
-- Name: translations_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.translations ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.translations_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: categories_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.categories_id_seq', 2, true);


--
-- Name: countries_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.countries_id_seq', 3, true);


--
-- Name: customers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: pg_database_owner
--

SELECT pg_catalog.setval('public.customers_id_seq', 1, true);


--
-- Name: orders_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.orders_id_seq', 1, false);


--
-- Name: products_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.products_id_seq', 1, false);


--
-- Name: tokens_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tokens_id_seq', 1, false);


--
-- Name: translations_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.translations_id_seq', 45, true);


--
-- Name: categories categories_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categories
    ADD CONSTRAINT categories_pkey PRIMARY KEY (id);


--
-- Name: customers customers_pkey; Type: CONSTRAINT; Schema: public; Owner: pg_database_owner
--

ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (id);


--
-- Name: orders orders_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (id);


--
-- Name: products products_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_pkey PRIMARY KEY (id);


--
-- Name: tokens tokens_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tokens
    ADD CONSTRAINT tokens_pkey PRIMARY KEY (id);


--
-- Name: translations translations_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.translations
    ADD CONSTRAINT translations_pkey PRIMARY KEY (id);


--
-- Name: ix_cat_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_cat_id ON public.categories USING btree (id) WITH (deduplicate_items='true');


--
-- Name: ix_cust_id; Type: INDEX; Schema: public; Owner: pg_database_owner
--

CREATE INDEX ix_cust_id ON public.customers USING btree (id) WITH (deduplicate_items='true');


--
-- Name: ix_email; Type: INDEX; Schema: public; Owner: pg_database_owner
--

CREATE INDEX ix_email ON public.customers USING btree (email) WITH (deduplicate_items='true');


--
-- Name: ix_prod_avail; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_prod_avail ON public.products USING btree (avalaible) WITH (deduplicate_items='true');


--
-- Name: ix_prod_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_prod_id ON public.products USING btree (id) WITH (deduplicate_items='true');


--
-- Name: ix_prod_published; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_prod_published ON public.products USING btree (published) WITH (deduplicate_items='true');


--
-- Name: ix_token_email; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_token_email ON public.tokens USING btree (email) WITH (deduplicate_items='true');


--
-- Name: ix_token_token; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_token_token ON public.tokens USING btree (token) WITH (deduplicate_items='true');


--
-- Name: ix_token_valid; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_token_valid ON public.tokens USING btree (valid_until) WITH (deduplicate_items='true');


--
-- Name: ix_trans_key; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_trans_key ON public.translations USING btree (key) WITH (deduplicate_items='true');


--
-- Name: ix_trans_lang; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_trans_lang ON public.translations USING btree (lang) WITH (deduplicate_items='true');


--
-- Name: products fk_prod_cat; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT fk_prod_cat FOREIGN KEY (category_id) REFERENCES public.categories(id) NOT VALID;


--
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;
GRANT ALL ON SCHEMA public TO PUBLIC;


--
-- PostgreSQL database dump complete
--

