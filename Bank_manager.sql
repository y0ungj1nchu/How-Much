------------------------------------------------------------
-- 0) 기존 객체 제거 (DROP)
------------------------------------------------------------

-- SEQUENCES
BEGIN EXECUTE IMMEDIATE 'DROP SEQUENCE SEQ_ASSET'; EXCEPTION WHEN OTHERS THEN NULL; END;
/
BEGIN EXECUTE IMMEDIATE 'DROP SEQUENCE SEQ_METHOD'; EXCEPTION WHEN OTHERS THEN NULL; END;
/
BEGIN EXECUTE IMMEDIATE 'DROP SEQUENCE SEQ_CATS'; EXCEPTION WHEN OTHERS THEN NULL; END;
/
BEGIN EXECUTE IMMEDIATE 'DROP SEQUENCE SEQ_SUB_CATS'; EXCEPTION WHEN OTHERS THEN NULL; END;
/
BEGIN EXECUTE IMMEDIATE 'DROP SEQUENCE SEQ_FIXED_EXP'; EXCEPTION WHEN OTHERS THEN NULL; END;
/
BEGIN EXECUTE IMMEDIATE 'DROP SEQUENCE SEQ_TX'; EXCEPTION WHEN OTHERS THEN NULL; END;
/
BEGIN EXECUTE IMMEDIATE 'DROP SEQUENCE SEQ_BUDGETS'; EXCEPTION WHEN OTHERS THEN NULL; END;
/

-- TABLES
BEGIN EXECUTE IMMEDIATE 'DROP TABLE INSTALLMENTS CASCADE CONSTRAINTS'; EXCEPTION WHEN OTHERS THEN NULL; END;
/
BEGIN EXECUTE IMMEDIATE 'DROP TABLE TRANSACTIONS CASCADE CONSTRAINTS'; EXCEPTION WHEN OTHERS THEN NULL; END;
/
BEGIN EXECUTE IMMEDIATE 'DROP TABLE FIXED_EXPENSES CASCADE CONSTRAINTS'; EXCEPTION WHEN OTHERS THEN NULL; END;
/
BEGIN EXECUTE IMMEDIATE 'DROP TABLE BUDGETS CASCADE CONSTRAINTS'; EXCEPTION WHEN OTHERS THEN NULL; END;
/
BEGIN EXECUTE IMMEDIATE 'DROP TABLE SUB_CATEGORIES CASCADE CONSTRAINTS'; EXCEPTION WHEN OTHERS THEN NULL; END;
/
BEGIN EXECUTE IMMEDIATE 'DROP TABLE CATEGORIES CASCADE CONSTRAINTS'; EXCEPTION WHEN OTHERS THEN NULL; END;
/
BEGIN EXECUTE IMMEDIATE 'DROP TABLE PAY_METHODS CASCADE CONSTRAINTS'; EXCEPTION WHEN OTHERS THEN NULL; END;
/
BEGIN EXECUTE IMMEDIATE 'DROP TABLE ASSETS CASCADE CONSTRAINTS'; EXCEPTION WHEN OTHERS THEN NULL; END;
/

------------------------------------------------------------
-- 1) 시퀀스 생성
------------------------------------------------------------

CREATE SEQUENCE SEQ_ASSET     START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_METHOD    START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_CATS      START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_SUB_CATS  START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_FIXED_EXP START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_TX        START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_BUDGETS   START WITH 1 INCREMENT BY 1;

------------------------------------------------------------
-- 2) 테이블 생성
------------------------------------------------------------

------------------------------------------------------------
-- ASSETS (자산)
------------------------------------------------------------

CREATE TABLE ASSETS (
    ASSET_ID    NUMBER        NOT NULL,
    NAME        VARCHAR2(50)  NOT NULL,
    TYPE        VARCHAR2(20)  NOT NULL,  -- BANK / CASH / SECURITIES
    BALANCE     NUMBER        DEFAULT 0,
    CONSTRAINT PK_ASSETS PRIMARY KEY (ASSET_ID)
);

------------------------------------------------------------
-- PAY_METHODS (거래수단)
------------------------------------------------------------

CREATE TABLE PAY_METHODS (
    METHOD_ID   NUMBER        NOT NULL,
    NAME        VARCHAR2(50)  NOT NULL,
    TYPE        VARCHAR2(20)  NOT NULL,  -- CREDIT / CHECK / CASH / TRANSFER / SIMPLEPAY
    ASSET_ID    NUMBER        NULL,      -- CHECK/CASH/TRANSFER 연결
    BILLING_DAY NUMBER        NULL,
    CONSTRAINT PK_METHODS PRIMARY KEY (METHOD_ID),
    CONSTRAINT FK_METHOD_ASSET FOREIGN KEY (ASSET_ID)
        REFERENCES ASSETS(ASSET_ID)
);

------------------------------------------------------------
-- 카테고리
------------------------------------------------------------

CREATE TABLE CATEGORIES (
    CATEGORY_ID  NUMBER        NOT NULL,
    NAME         VARCHAR2(50)  NOT NULL,
    TYPE         VARCHAR2(10)  NOT NULL,   -- EXPENSE / INCOME
    CONSTRAINT PK_CATEGORIES PRIMARY KEY (CATEGORY_ID)
);

------------------------------------------------------------
-- 서브카테고리
------------------------------------------------------------

CREATE TABLE SUB_CATEGORIES (
    SUB_ID        NUMBER        NOT NULL,
    CATEGORY_ID   NUMBER        NOT NULL,
    NAME          VARCHAR2(50)  NOT NULL,
    CONSTRAINT PK_SUB_CATEGORIES PRIMARY KEY (SUB_ID),
    CONSTRAINT FK_SUB_CATEGORY FOREIGN KEY (CATEGORY_ID)
        REFERENCES CATEGORIES(CATEGORY_ID)
);

------------------------------------------------------------
-- 예산
------------------------------------------------------------

CREATE TABLE BUDGETS (
    BUDGET_ID    NUMBER       NOT NULL,
    CATEGORY_ID  NUMBER       NOT NULL,
    YYYYMM       VARCHAR2(6)  NOT NULL,
    AMOUNT       NUMBER       DEFAULT 0,
    CONSTRAINT PK_BUDGETS PRIMARY KEY (BUDGET_ID),
    CONSTRAINT FK_BUDGET_CATEGORY FOREIGN KEY (CATEGORY_ID)
        REFERENCES CATEGORIES(CATEGORY_ID)
);

------------------------------------------------------------
-- 정기 지출
------------------------------------------------------------

CREATE TABLE FIXED_EXPENSES (
    FIXED_EXP_ID NUMBER          NOT NULL,
    SUB_ID       NUMBER          NOT NULL,
    METHOD_ID    NUMBER          NOT NULL,
    NAME         VARCHAR2(100)   NOT NULL,
    AMOUNT       NUMBER          NOT NULL,
    CYCLE_TYPE   VARCHAR2(10)    DEFAULT 'MONTHLY',
    CYCLE_DAY    NUMBER          NOT NULL,
    START_DATE   DATE            DEFAULT SYSDATE,
    END_DATE     DATE            DEFAULT NULL,
    CONSTRAINT PK_FIXED_EXP PRIMARY KEY (FIXED_EXP_ID),
    CONSTRAINT FK_FIXED_SUB FOREIGN KEY (SUB_ID)
        REFERENCES SUB_CATEGORIES(SUB_ID),
    CONSTRAINT FK_FIXED_METHOD FOREIGN KEY (METHOD_ID)
        REFERENCES PAY_METHODS(METHOD_ID)
);

------------------------------------------------------------
-- 거래내역 (Transactions)
------------------------------------------------------------

CREATE TABLE TRANSACTIONS (
    TX_ID         NUMBER         NOT NULL,
    METHOD_ID     NUMBER         NOT NULL,
    ASSET_ID      NUMBER         NULL,     -- 신용카드는 NULL
    SUB_ID        NUMBER         NOT NULL,
    FIXED_EXP_ID  NUMBER         NULL,
    AMOUNT        NUMBER         NOT NULL,
    TX_DATE       DATE           NOT NULL,
    MEMO          VARCHAR2(200),
    CREATED_AT    DATE           DEFAULT SYSDATE,
    CONSTRAINT PK_TX PRIMARY KEY (TX_ID),
    CONSTRAINT FK_TX_METHOD FOREIGN KEY (METHOD_ID)
        REFERENCES PAY_METHODS(METHOD_ID),
    CONSTRAINT FK_TX_ASSET FOREIGN KEY (ASSET_ID)
        REFERENCES ASSETS(ASSET_ID),
    CONSTRAINT FK_TX_SUB FOREIGN KEY (SUB_ID)
        REFERENCES SUB_CATEGORIES(SUB_ID),
    CONSTRAINT FK_TX_FIXED FOREIGN KEY (FIXED_EXP_ID)
        REFERENCES FIXED_EXPENSES(FIXED_EXP_ID)
);

------------------------------------------------------------
-- 할부 정보
------------------------------------------------------------

CREATE TABLE INSTALLMENTS (
    TX_ID          NUMBER        NOT NULL,
    TOTAL_MONTHS   NUMBER        NOT NULL,
    CURRENT_MONTH  NUMBER        NOT NULL,
    CONSTRAINT PK_INSTALLMENTS PRIMARY KEY (TX_ID),
    CONSTRAINT FK_INSTALL_TX FOREIGN KEY (TX_ID)
        REFERENCES TRANSACTIONS(TX_ID) ON DELETE CASCADE
);

------------------------------------------------------------
-- 3) 초기 데이터 INSERT
------------------------------------------------------------

------------------------------------------------------------
-- 자산
------------------------------------------------------------

INSERT INTO ASSETS VALUES (1, '신한은행', 'BANK', 5000000);
INSERT INTO ASSETS VALUES (2, '현금', 'CASH', 30000);

------------------------------------------------------------
-- 거래수단
------------------------------------------------------------

INSERT INTO PAY_METHODS VALUES (1, '현대카드', 'CREDIT', NULL, 25);
INSERT INTO PAY_METHODS VALUES (2, '신한 체크카드', 'CHECK', 1, NULL);
INSERT INTO PAY_METHODS VALUES (3, '현금', 'CASH', 2, NULL);
INSERT INTO PAY_METHODS VALUES (4, '계좌이체', 'TRANSFER', 1, NULL);

------------------------------------------------------------
-- 카테고리
------------------------------------------------------------

INSERT INTO CATEGORIES VALUES (1, '식비', 'EXPENSE');
INSERT INTO CATEGORIES VALUES (2, '교통', 'EXPENSE');
INSERT INTO CATEGORIES VALUES (3, '쇼핑', 'EXPENSE');
INSERT INTO CATEGORIES VALUES (4, '급여', 'INCOME');

------------------------------------------------------------
-- 서브카테고리
------------------------------------------------------------

INSERT INTO SUB_CATEGORIES VALUES (1, 1, '외식');
INSERT INTO SUB_CATEGORIES VALUES (2, 1, '카페/베이커리');
INSERT INTO SUB_CATEGORIES VALUES (3, 1, '배달음식');
INSERT INTO SUB_CATEGORIES VALUES (4, 2, '버스');
INSERT INTO SUB_CATEGORIES VALUES (5, 2, '지하철');
INSERT INTO SUB_CATEGORIES VALUES (6, 3, '의류');
INSERT INTO SUB_CATEGORIES VALUES (7, 3, '잡화');
INSERT INTO SUB_CATEGORIES VALUES (8, 4, '정기급여');
INSERT INTO SUB_CATEGORIES VALUES (9, 4, '상여금');

------------------------------------------------------------
-- 예산
------------------------------------------------------------

INSERT INTO BUDGETS VALUES (SEQ_BUDGETS.NEXTVAL, 1, '202511', 500000);
INSERT INTO BUDGETS VALUES (SEQ_BUDGETS.NEXTVAL, 2, '202511', 150000);
INSERT INTO BUDGETS VALUES (SEQ_BUDGETS.NEXTVAL, 3, '202511', 1000000);

------------------------------------------------------------
-- 정기 지출
------------------------------------------------------------

INSERT INTO FIXED_EXPENSES VALUES (
    SEQ_FIXED_EXP.NEXTVAL, 
    7, 
    1, 
    '넷플릭스',
    17000,
    'MONTHLY',
    1,
    SYSDATE,
    NULL
);

------------------------------------------------------------
-- 거래내역
------------------------------------------------------------

-- 11월 급여 (계좌입금)
INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL, 
    4, -- 계좌이체
    1, -- 신한은행
    8,
    NULL,
    3000000,
    TO_DATE('2025-11-10 09:00','YYYY-MM-DD HH24:MI'),
    '11월 급여',
    SYSDATE
);

-- 점심 (신한 체크카드)
INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL,
    2,
    1,
    1,
    NULL,
    9000,
    TO_DATE('2025-11-11 12:30','YYYY-MM-DD HH24:MI'),
    '점심',
    SYSDATE
);

-- 버스 충전 (현금)
INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL,
    3,
    2,
    4,
    NULL,
    15000,
    TO_DATE('2025-11-12 23:40','YYYY-MM-DD HH24:MI'),
    '버스 충전',
    SYSDATE
);

-- 배달 라면 (신한 체크카드)
INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL,
    2,
    1,
    3,
    NULL,
    4500,
    TO_DATE('2025-11-13 13:00','YYYY-MM-DD HH24:MI'),
    '배달 라면',
    SYSDATE
);

-- 의류 구매 (신용카드)
INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL,
    1,
    NULL,
    6,
    NULL,
    1200000,
    TO_DATE('2025-11-15 15:00','YYYY-MM-DD HH24:MI'),
    '의류 구매',
    SYSDATE
);

INSERT INTO INSTALLMENTS VALUES (SEQ_TX.CURRVAL, 3, 1);

-- 넷플릭스 자동결제
INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL,
    1,
    NULL,
    7,
    1,
    17000,
    TO_DATE('2025-11-01 10:00','YYYY-MM-DD HH24:MI'),
    '넷플릭스 자동결제',
    SYSDATE
);

-- 12월 점심
INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL,
    2,
    1,
    1,
    NULL,
    8500,
    TO_DATE('2025-12-01 12:20','YYYY-MM-DD HH24:MI'),
    '점심 외식',
    SYSDATE
);

-- 커피
INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL,
    3,
    2,
    2,
    NULL,
    4800,
    TO_DATE('2025-12-02 09:10','YYYY-MM-DD HH24:MI'),
    '출근 커피',
    SYSDATE
);

-- 버스
INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL,
    3,
    2,
    4,
    NULL,
    1250,
    TO_DATE('2025-12-03 08:45','YYYY-MM-DD HH24:MI'),
    '버스',
    SYSDATE
);

-- 배달
INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL,
    2,
    1,
    3,
    NULL,
    18500,
    TO_DATE('2025-12-04 18:40','YYYY-MM-DD HH24:MI'),
    '저녁 배달',
    SYSDATE
);

-- 월급
INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL,
    4,
    1,
    8,
    NULL,
    3000000,
    TO_DATE('2025-12-05 08:00','YYYY-MM-DD HH24:MI'),
    '12월 급여',
    SYSDATE
);

COMMIT;