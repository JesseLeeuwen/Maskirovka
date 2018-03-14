using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka.News
{
    [CreateAssetMenu(menuName = "simple processor")]
    public class SimpleProcessor : NewsProcessor
    {
        public Vector3 biasValue;
        private bool succes;


        public override bool ProccesNews(News news)
        {
            float valueNews = Random.value * 100;
            float valueBias = Random.value * 100;
            biasValue = GameObject.FindWithTag("Manager").GetComponent<NewsManager>().bias;


            if (news.catagorie == Catagorie.A)
            {
                if (valueNews < 50)
                {
                    if (valueBias < biasValue.x && valueNews < news.chanceOfSucces)
                    {
                        succes = true;
                    }
                    else
                    {
                        biasValue.x = biasValue.x + news.biasChanger;
                        succes = false;                      
                    }
                }
                else if (valueNews >= 50)
                {
                    if (valueBias > biasValue.x && valueNews < news.chanceOfSucces)
                    {
                        succes = true;
                    }
                    else
                    {
                        biasValue.x = biasValue.x - news.biasChanger;
                        succes = false;
                    }
                }
            }

            else if (news.catagorie == Catagorie.B)
            {
                if (valueNews < 50)
                {
                    if (valueBias < biasValue.y && valueNews < news.chanceOfSucces)
                    {
                        succes = true;
                    }
                    else
                    {
                        biasValue.y = biasValue.y + news.biasChanger;
                        succes = false;                       
                    }
                }
                else if (valueNews >= 50)
                {
                    if (valueBias > biasValue.y && valueNews < news.chanceOfSucces)
                    {
                        succes = true;
                    }
                    else
                    {
                        biasValue.y = biasValue.y - news.biasChanger;
                        succes = false;                        
                    }
                }
            }

            else if (news.catagorie == Catagorie.C)
            {
                if (valueNews < 50)
                {
                    if (valueBias < biasValue.z && valueNews < news.chanceOfSucces)
                    {
                        succes = true;
                    }
                    else
                    {
                        biasValue.z = biasValue.z + news.biasChanger;
                        succes = false;                       
                    }
                }
                else if (valueNews >= 50)
                {
                    if (valueBias > biasValue.z && valueNews < news.chanceOfSucces)
                    {
                        succes = true;
                    }
                    else
                    {
                        biasValue.z = biasValue.z - news.biasChanger;
                        succes = false;
                    }
                }
            }

            if (succes)
            {
                foreach (Neighbour Neighbour in news.country.neighbours)
                {
                    Neighbour.neighbour.UpdateRepu(news.country, news.value, news.catagorie);
                }
            }
            GameObject.FindWithTag("Manager").GetComponent<NewsManager>().bias = biasValue;
            return succes;
        }
	}
}