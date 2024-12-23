const Home = () => {
  return (
    <div className="text-center">
      <h1 className="display-4">Головна сторінка</h1>
      <p>
        Цей застосунок дозволяє виконувати лабораторні роботи №1, №2 та №3. Для доступу до них необхідно авторизуватися та перейти на сторінку профілю.
      </p>
      <p>
        Для роботи з базою даних слід перейти на сторінку профілю. На цій сторінці ви зможете перейти до розділів,
        де буде представлена інформація про окремі таблиці у вигляді списку. Клікнувши на рядок таблиці,
        ви потрапите на сторінку конкретного елемента, де буде відображено детальнішу інформацію.
      </p>
    </div>
  );
}

export default Home;