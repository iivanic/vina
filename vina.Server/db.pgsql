
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
-- Data for Name: categories; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.categories (id, name_translation_key) FROM stdin;
1	pukli_kamen_cat
2	dvije_ruze_cat
\.


--
-- Data for Name: countries; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.countries (id, name) FROM stdin;
1	Hrvastka/Croatia
2	Österreich/Austria
3	Deutschland/Germany
\.


--
-- Data for Name: customers; Type: TABLE DATA; Schema: public; Owner: pg_database_owner
--

COPY public.customers (id, email, is_admin) FROM stdin;
1	info@vina-ivanic.hr	t
\.


--
-- Data for Name: orders; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.orders (id, order_time, payment_recieved, package_sent, package_recieveded, closed, amount, phone_for_delivery, country, state, city, address1, address2, comment) FROM stdin;
\.


--
-- Data for Name: products; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.products (id, name_translation_key, description_translation_key, price, max_order, avalaible, published, category_id, full_translation_key) FROM stdin;
1	pk_21	pk_21_desc	17	6	t	t	1	pk_21_full
2	pk_22	pk_22_desc	17	6	t	t	1	pk_21_full
3	dr_21	dr_21_desc	17	6	t	t	2	pk_21_full
4	dr_15	dr_15_desc	17	6	t	t	2	pk_15_full
\.


--
-- Data for Name: tokens; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tokens (id, email, valid_from, valid_until, token) FROM stdin;
\.


--
-- Data for Name: translations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.translations (id, key, content, lang) FROM stdin;
1	pukli_kamen_cat	Pukli kamen	hr
2	pukli_kamen_cat	Cracked stone	en
3	pukli_kamen_cat	Pukli kamen	de
4	dvije_ruze_cat	Dvije ruže	hr
5	dvije_ruze_cat	Two roses	en
6	dvije_ruze_cat	Zwei Rosen	de
7	pk_21	Pukli kamen 2021	hr
8	pk_21	Cracked stone 2021	en
9	pk_21	Pukli kamen 2021	de
10	pk_22	Pukli kamen 2022	hr
11	pk_22	Cracked stone 2022	en
12	pk_22	Pukli kamen 2022	de
13	dr_21	Dvije ruže 2021	hr
14	dr_21	Two roses	en
15	dr_21	Zwei Rosen	de
16	pk_21_desc	Our macerated wine made from our best grapes of our local variety "Skrlet", in which we treat its's delicate white grapes as if it was red variety. Fermented on skins until dryness with pigéage every 8 hrs, aged in used barique.	en
17	pk_21_desc	Unser mazerierter Wein wird aus unseren besten Trauben unserer lokalen Sorte "Skrlet" hergestellt, in der wir ihre zarten weißen Trauben behandeln, als wäre es eine rote Sorte. Gärung auf Schalen bis zur Trockenheit mit Schweinefleisch alle 8 Stunden, Reifung in gebrauchten Bariques.	de
18	pk_21_desc	Jantarno vino od našeg najboljeg škrleta. Vino autohtone sorte škrlet napravljeno tehnologijom crnog vina. Fermentirano na kožicama do suhoće uz pigéage svakih 8 sati, odležavano u rabljenim barrique bačvicama.	hr
19	pk_22_desc	Our macerated wine made from our best grapes of our local variety "Skrlet", in which we treat its's delicate white grapes as if it was red variety. Fermented on skins until dryness with pigéage every 8 hrs, aged in used barique.	en
20	pk_22_desc	Unser mazerierter Wein wird aus unseren besten Trauben unserer lokalen Sorte "Skrlet" hergestellt, in der wir ihre zarten weißen Trauben behandeln, als wäre es eine rote Sorte. Gärung auf Schalen bis zur Trockenheit mit Schweinefleisch alle 8 Stunden, Reifung in gebrauchten Bariques.	de
21	pk_22_desc	Jantarno vino od našeg najboljeg škrleta. Vino autohtone sorte škrlet napravljeno tehnologijom crnog vina. Fermentirano na kožicama do suhoće uz pigéage svakih 8 sati, odležavano u rabljenim barrique bačvicama.	hr
22	dr_21_desc	Our best pinot noir.	en
23	dr_21_desc	Unser bester Spätburgunder. Eine Hommage an das Burgund.	de
24	dr_21_desc	Naš najbolji pinot crni. Hommage Burgundiji.	hr
25	dr_15_desc	Our best pinot noir.	en
26	dr_15_desc	Unser bester Spätburgunder. Eine Hommage an das Burgund.	de
27	dr_15_desc	Naš najbolji pinot crni. Hommage Burgundiji.	hr
28	pk_21_full	Balance between varietal expression and extraction. 12% alc.	en
29	pk_21_full	Gleichgewicht zwischen sortenreiner Ausprägung und Extraktion. 12 % Alk.	de
30	pk_21_full	Balans sortnosti i ekstrakcije. 12% alkohola.	hr
31	pk_22_full	Structured. and ripe. 13% alc.	en
32	pk_22_full	Strukturiert. und reif. 13 % Alk.	de
33	pk_22_full	Strukturno i zrelo, 13% alkohola	hr
34	dr_21_full	Fresh, delicious. forest floor	en
35	dr_21_full	Frisch, lecker. Waldboden	de
36	dr_21_full	Svježe, slasno, šumski pod, 	hr
37	dr_15_full	Spicy notes and chocolate, ready	en
38	dr_15_full	Würzige Noten und Schokolade, fertig zum Trinken	de
39	dr_15_full	Začinske note i čokoloda, spremno	hr
40	mission	<p>Wine is not only an agricultural product. Wine represents a philosophy of life, it is a product with terroir.</p><p<Ultimately, our wines express the character of our vineyards, a character more persistent than the mood of an individual vintage.</p> 	hr
41	mission	<p>Vino nije samo poljoprivredni proizvod. Vino predstavlja životnu filozofiju, ono je proizvod s terroirom.</p><p<U konačnici, naša vina izražavaju osobnost naših vinograda, osobnost postojaniju od raspoloženja pojedine berbe.</p> 	en
42	mission	Wein ist nicht nur ein landwirtschaftliches Produkt. Wein stellt eine Lebensphilosophie dar, er ist ein Produkt mit Terroir.</p><p<Letztlich drücken unsere Weine den Charakter unserer Weinberge aus, der nachhaltiger ist als die Stimmung eines einzelnen Jahrgangs.</p> 	hr
43	title	Vinarija Ivanić iz Kutine	hr
44	title	Weingut Ivanic aus Kutina	de
45	title	Ivanić winery from Kutina	en
\.


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

