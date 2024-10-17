using System;

struct Subject
{
    public string name;
    public int Score;
    public int Credit;
}

namespace E94106012_practice_2_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool loopDone = true;
            Subject[] subjects = new Subject[0];

            do
            {
                Console.WriteLine();
                Console.WriteLine("## 成績計算器 ##");
                Console.WriteLine("1. 新增科目 (create)");
                Console.WriteLine("2. 刪除科目 (delete)");
                Console.WriteLine("3. 更新科目 (update)");
                Console.WriteLine("4. 列印成績單 (print)");
                Console.WriteLine("5. 退出選單 (exit)");
                Console.Write("輸入要執行的指令操作: ");

                string option = Console.ReadLine();
                string[] order = option.Split(' ');

                switch (order[0])
                {
                    case "create":
                        if (order.Length == 4)
                        {
                            int Score = int.Parse(order[2]);
                            int Credit = int.Parse(order[3]);
                            bool duplicateSubject = false;

                            foreach (Subject subject in subjects)
                            {
                                if (subject.name == order[1])
                                {
                                    duplicateSubject = true;
                                    break;
                                }
                            }

                            if (duplicateSubject)
                            {
                                Console.WriteLine("已存在相同的科目");
                                break;
                            }

                            if (Score >= 0 && Score <= 100)
                            {
                                if (Credit >= 0 && Credit <= 10)
                                {
                                    Array.Resize(ref subjects, subjects.Length + 1);
                                    subjects[subjects.Length - 1].name = order[1];
                                    subjects[subjects.Length - 1].Score = Score;
                                    subjects[subjects.Length - 1].Credit = Credit;
                                    Console.WriteLine("科目已新增");
                                }
                                else
                                {
                                    Console.WriteLine("學分數不合法! 請重新輸入!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("成績不合法! 請重新輸入!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("指令格式不符合要求! 請重新輸入!");
                        }
                        break;

                    case "delete":
                        if (order.Length == 2)
                        {
                            bool subjectExists = false;

                            foreach (Subject subject in subjects)
                            {
                                if (subject.name == order[1])
                                {
                                    subjectExists = true;
                                    break;
                                }
                            }

                            if (!subjectExists)
                            {
                                Console.WriteLine("科目不存在");
                            }
                            else
                            {
                                subjects = subjects.Where(val => val.name != order[1]).ToArray();
                                Console.WriteLine("科目已刪除");
                            }
                        }
                        else
                        {
                            Console.WriteLine("指令格式不符合要求! 請重新輸入!");
                        }
                        break;

                    case "update":
                        if (order.Length == 4)
                        {
                            int updateScore = int.Parse(order[2]);
                            int updateCredit = int.Parse(order[3]);

                            if (updateScore >= 0 && updateScore <= 100)
                            {
                                if (updateCredit >= 0 && updateCredit <= 10)
                                {
                                    bool subjectToUpdateExists = false;

                                    for (int i = 0; i < subjects.Length; i++)
                                    {
                                        if (subjects[i].name == order[1])
                                        {
                                            subjects[i].Score = updateScore;
                                            subjects[i].Credit = updateCredit;
                                            Console.WriteLine("科目已更新");
                                            subjectToUpdateExists = true;
                                        }
                                    }

                                    if (!subjectToUpdateExists)
                                    {
                                        Console.WriteLine("科目不存在");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("新學分數不合法! 請重新輸入!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("新成績不合法! 請重新輸入!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("指令格式不符合要求! 請重新輸入!");
                        }
                        break;

                    case "print":
                        if (order.Length == 1)
                        {
                            if (subjects.Length > 1)
                            {
                                Sort(ref subjects);
                            }

                            int totalScore = 0;
                            int totalCredit = 0;

                            Console.WriteLine();
                            Console.WriteLine("我的成績單:");
                            Console.WriteLine("編號\t科目代碼\t分數\t等第\t學分數");

                            if (subjects.Length > 0)
                            {
                                for (int i = 0; i < subjects.Length; i++)
                                {
                                    Console.WriteLine("{0}\t{1}\t\t{2}\t{3}\t{4}",
                                        i + 1, subjects[i].name, subjects[i].Score, CalLevel(subjects[i].Score), subjects[i].Credit);

                                    totalScore += subjects[i].Score;
                                    totalCredit += subjects[i].Credit;
                                }

                                Console.WriteLine("總平均分: {0}", Average(subjects));
                                Console.WriteLine("GPA: {0}/4.0 (舊制), {1}/4.3 (新制)", OldGPA(subjects), NewGPA(subjects));
                                Console.WriteLine("已取得學分/總學分: {0}/{1}", GetCredit(subjects), totalCredit);
                            }
                        }
                        else
                        {
                            Console.WriteLine("指令格式不符合要求! 請重新輸入!");
                        }
                        break;

                    case "exit":
                        if (order.Length == 1)
                        {
                            loopDone = false;
                        }
                        else
                        {
                            Console.WriteLine("指令格式不符合要求! 請重新輸入!");
                        }
                        break;

                    default:
                        Console.WriteLine("無效的指令! 請重新輸入!");
                        break;
                }
            } while (loopDone);

            Console.ReadKey();
        }

        private static void Sort(ref Subject[] subArr)
        {
            for (int i = 0; i < subArr.Length - 1; i++)
            {
                for (int j = i + 1; j < subArr.Length; j++)
                {
                    if (subArr[i].Score < subArr[j].Score)
                    {
                        Swap(ref subArr[i], ref subArr[j]);
                    }
                }
            }
        }

        private static void Swap(ref Subject S1, ref Subject S2)
        {
            Subject temp;
            temp = S1;
            S1 = S2;
            S2 = temp;
        }

        private static string CalLevel(int Score)
        {
            if (Score <= 100 && Score >= 90)
            {
                return "A+";
            }
            else if (Score < 90 && Score >= 85)
            {
                return "A";
            }
            else if (Score < 85 && Score >= 80)
            {
                return "A-";
            }
            else if (Score < 80 && Score >= 77)
            {
                return "B+";
            }
            else if (Score < 77 && Score >= 73)
            {
                return "B";
            }
            else if (Score < 73 && Score >= 70)
            {
                return "B-";
            }
            else if (Score < 70 && Score >= 67)
            {
                return "C+";
            }
            else if (Score < 67 && Score >= 63)
            {
                return "C";
            }
            else if (Score < 63 && Score >= 60)
            {
                return "C-";
            }
            else
            {
                return "F";
            }
        }

        private static string Average(Subject[] subjects)
        {
            int sum = 0;
            int totalCredit = 0;

            foreach (Subject subject in subjects)
            {
                sum += (subject.Score * subject.Credit);
                totalCredit += subject.Credit;
            }

            float average = (float)sum / totalCredit;
            return average.ToString("#.#");
        }

        private static string OldGPA(Subject[] subjects)
        {
            int sum = 0;
            int totalCredit = 0;

            foreach (Subject subject in subjects)
            {
                if (subject.Score <= 100 && subject.Score >= 80)
                {
                    sum += (subject.Credit * 4);
                }
                else if (subject.Score < 80 && subject.Score >= 70)
                {
                    sum += (subject.Credit * 3);
                }
                else if (subject.Score < 70 && subject.Score >= 60)
                {
                    sum += (subject.Credit * 2);
                }
                else if (subject.Score < 60 && subject.Score >= 50)
                {
                    sum += (subject.Credit * 1);
                }

                totalCredit += subject.Credit;
            }

            float avg = (float)sum / totalCredit;
            return avg.ToString("#.#");
        }

        private static string NewGPA(Subject[] subjects)
        {
            double sum = 0;
            int totalCredit = 0;

            foreach (Subject subject in subjects)
            {
                if (subject.Score <= 100 && subject.Score >= 90)
                {
                    sum += (subject.Credit * 4.3);
                }
                else if (subject.Score < 90 && subject.Score >= 85)
                {
                    sum += (subject.Credit * 4.0);
                }
                else if (subject.Score < 85 && subject.Score >= 80)
                {
                    sum += (subject.Credit * 3.7);
                }
                else if (subject.Score < 80 && subject.Score >= 77)
                {
                    sum += (subject.Credit * 3.3);
                }
                else if (subject.Score < 77 && subject.Score >= 73)
                {
                    sum += (subject.Credit * 3.0);
                }
                else if (subject.Score < 73 && subject.Score >= 70)
                {
                    sum += (subject.Credit * 2.7);
                }
                else if (subject.Score < 70 && subject.Score >= 67)
                {
                    sum += (subject.Credit * 2.3);
                }
                else if (subject.Score < 67 && subject.Score >= 63)
                {
                    sum += (subject.Credit * 2.0);
                }
                else if (subject.Score < 63 && subject.Score >= 60)
                {
                    sum += (subject.Credit * 1.7);
                }

                totalCredit += subject.Credit;
            }

            double avg = sum / totalCredit;
            return avg.ToString("#.#");
        }

        private static int GetCredit(Subject[] subjects)
        {
            int totalCredit = 0;

            foreach (Subject subject in subjects)
            {
                if (subject.Score >= 60)
                {
                    totalCredit += subject.Credit;
                }
            }

            return totalCredit;
        }
    }
}
