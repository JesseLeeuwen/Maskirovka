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
            float bias = news.biasChanger;




            if (news.catagorie == Catagorie.A)
            {
                if (news.value < 50)
                {
                    if(biasValue.x < valueBias && news.chanceOfSucces < valueNews)
                    {
                        Debug.Log(biasValue.x);
                        Debug.Log(valueBias);
                        Debug.Log("Smaller");
                        succes = true;
                    }
                    else
                    {
                        biasValue.x = biasValue.x - bias;
                        succes = false;
                    }
                }
                if (news.value >= 50)
                {
                    if (biasValue.x > valueBias && news.chanceOfSucces < valueNews)
                    {
                        Debug.Log(biasValue.x);
                        Debug.Log(valueBias);
                        Debug.Log("Bigger");
                        succes = true;
                    }
                    else
                    {
                        biasValue.x = biasValue.x + bias;
                        succes = false;
                    }
                }
            }

            if (news.catagorie == Catagorie.B)
            {
                if (news.value < 50)
                {
                    if (biasValue.y < valueBias && news.chanceOfSucces < valueNews)
                    {
                        succes = true;
                    }
                    else
                    {
                        biasValue.y = biasValue.y - bias;
                        succes = false;
                    }
                }
                if (news.value >= 50)
                {
                    if (biasValue.y > valueBias && news.chanceOfSucces < valueNews)
                    {
                        succes = true;
                    }
                    else
                    {
                        biasValue.y = biasValue.y + bias;
                        succes = false;
                    }
                }
            }

            if (news.catagorie == Catagorie.C)
            {
                if (news.value < 50)
                {
                    if (biasValue.z < valueBias && news.chanceOfSucces < valueNews)
                    {
                        succes = true;
                    }
                    else
                    {
                        biasValue.z = biasValue.z - bias;
                        succes = false;
                    }
                }
                if (news.value >= 50)
                {
                    if (biasValue.z > valueBias && news.chanceOfSucces < valueNews)
                    {
                        succes = true;
                    }
                    else
                    {
                        biasValue.z = biasValue.z + bias;
                        succes = false;
                    }
                }
            }

            //BYPASS BIAS SYSTEM
            succes =  valueNews < news.chanceOfSucces;

            if (succes)
            {
                foreach (Neighbour Neighbour in news.country.neighbours)
                {
                    Neighbour.neighbour.UpdateRepu(news.country, news.value, news.catagorie);
                    return succes;
                }
            }
            GameObject.FindWithTag("Manager").GetComponent<NewsManager>().bias = biasValue;
            return succes;
        }
	}
}