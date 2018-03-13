using UnityEngine;

namespace Maskirovka.News
{
    [CreateAssetMenu(menuName = "simple processor")]
    public class SimpleProcessor : NewsProcessor
    {
        public Vector3 bias;
        private bool succes;


        public override bool ProccesNews(News news)
        {
            float valueNews = Random.value * 100;
            float valueBias = Random.value * 100;
            bias = news.currentBias;


            if (news.catagorie == Catagorie.A)
            {
                if (valueNews < 50)
                {
                    if (valueBias < bias.x && valueNews < news.chanceOfSucces)
                    {
                        news.currentBias.x = bias.x - news.biasChanger;
                        succes = true;
                    }
                    else
                    {
                        news.currentBias.x = bias.x + news.biasChanger;
                        succes = false;                      
                    }
                }
                else if (valueNews >= 50)
                {
                    if (valueBias > bias.x && valueNews < news.chanceOfSucces)
                    {
                        news.currentBias.x = bias.x + news.biasChanger;
                        succes = true;
                    }
                    else
                    {
                        news.currentBias.x = bias.x - news.biasChanger;
                        succes = false;
                    }
                }
            }

            if (news.catagorie == Catagorie.B)
            {
                if (valueNews < 50)
                {
                    if (valueBias < bias.y && valueNews < news.chanceOfSucces)
                    {
                        news.currentBias.y = bias.y - news.biasChanger;
                        succes = true;
                    }
                    else
                    {
                        news.currentBias.y = bias.y + news.biasChanger;
                        succes = false;                       
                    }
                }
                else if (valueNews >= 50)
                {
                    if (valueBias > bias.y && valueNews < news.chanceOfSucces)
                    {
                        news.currentBias.y = bias.y + news.biasChanger;
                        succes = true;
                    }
                    else
                    {
                        news.currentBias.y = bias.y - news.biasChanger;
                        succes = false;                        
                    }
                }
            }

            if (news.catagorie == Catagorie.C)
            {
                if (valueNews < 50)
                {
                    if (valueBias < bias.z && valueNews < news.chanceOfSucces)
                    {
                        news.currentBias.z = bias.z - news.biasChanger;
                        succes = true;
                    }
                    else
                    {
                        news.currentBias.z = bias.z + news.biasChanger;
                        succes = false;                       
                    }
                }
                else if (valueNews >= 50)
                {
                    if (valueBias > bias.z && valueNews < news.chanceOfSucces)
                    {
                        news.currentBias.z = bias.z + news.biasChanger;
                        succes = true;
                    }
                    else
                    {
                        news.currentBias.z = bias.z - news.biasChanger;
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
            return valueNews < news.chanceOfSucces;
        }
	}
}