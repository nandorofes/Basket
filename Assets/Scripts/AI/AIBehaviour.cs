using System;
using System.Collections;
using UnityEngine;

public abstract class AIBehaviour : MonoBehaviour
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Tiempo de respuesta de la inteligencia artificial
    public float aiUpdateTimeMin = 0.4f;
    public float aiUpdateTimeMax = 0.6f;

    // Estado emocional de la inteligencia artificial
    protected float aiEmotion;
    protected AnimationCurve aiEmotionCurve;

    // Nivel de profesionalidad de la inteligencia artificial
    protected float aiSkillLevel;

    // Personalidad de la inteligencia artificial
    protected float aiConcentration;
    protected float aiCompetitiveness;
    protected float aiEmotionalBias;

    // Estado de ejecución
    protected bool alive;

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Valores de control
    public float AISkillLevel
    {
        get { return this.aiSkillLevel; }
        set { this.aiSkillLevel = Mathf.Clamp01(value); }
    }

    public float AIConcentration
    {
        get { return this.aiConcentration; }
        set { this.aiConcentration = Mathf.Clamp01(value); }
    }

    public float AICompetitiveness
    {
        get { return this.aiCompetitiveness; }
        set { this.aiCompetitiveness = Mathf.Clamp01(value); }
    }

    public float AIEmotionalBias
    {
        get { return this.aiEmotionalBias; }
        set { this.aiEmotionalBias = Mathf.Clamp01(value); }
    }

    // Valores calculados
    public AIEmotion AIEmotion
    {
        get
        {
            if (this.aiEmotion < -0.6f)
                return AIEmotion.Frustrated;
            else if (this.aiEmotion < -0.2f)
                return AIEmotion.Competitive;
            else if (this.aiEmotion < 0.2f)
                return AIEmotion.Normal;
            else if (this.aiEmotion < 0.6f)
                return AIEmotion.Determined;
            else
                return AIEmotion.Overconfident;
        }
    }

    public float AIEmotionValue
    {
        get { return this.aiEmotion; }
    }

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Eventos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public event Action<float> OnAIOutput = delegate { };

    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos de MonoBehaviour
    protected void Awake()
    {
        this.alive = true;
    }

    protected void OnDestroy()
    {
        this.alive = false;
    }

    // Métodos de control
    public void Activate()
    {
        this.aiEmotionCurve = this.GenerateSuccessCurve();
        this.StartCoroutine(this.AICorroutine());
    }

    public void Deactivate()
    {
        Debug.Log("Deactivating...");
        this.StopAllCoroutines();
    }

    public void GenerateRandomPersonality()
    {
        this.aiSkillLevel = UnityEngine.Random.value;

        this.aiConcentration = AIBehaviour.RandomGaussian(0.5f, 0.25f);
        this.aiCompetitiveness = AIBehaviour.RandomGaussian(0.5f, 0.25f);
        this.aiEmotionalBias = AIBehaviour.RandomGaussian(0.5f, 0.25f);

        this.aiEmotion = 0.0f;
    }

    // Métodos abstractos
    //protected abstract float CalculateAdvantage();

    // Métodos auxiliares
    protected static float SigmoidFunction(float slope, float centerpoint, float value)
    {
        return 2.0f * (1.0f / (1.0f + Mathf.Exp(-slope * (value - centerpoint)))) - 1.0f;
    }

    protected static float ClampTo(float f, float min, float max)
    {
        if (min < max)
            return (f < min) ? min : (f > max ? max : f);
        return (f < max) ? max : (f > min ? min : f);
    }

    protected static float RandomGaussian(float mu, float sigma)
    {
        float u1 = UnityEngine.Random.value;
        float u2 = UnityEngine.Random.value;

        float rand_std_normal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);

        return mu + sigma * rand_std_normal;
    }

    protected AnimationCurve GenerateSuccessCurve()
    {
        float effSkillLevel = Mathf.Clamp01(this.aiSkillLevel);
        float effConcentration = Mathf.Clamp01(this.aiConcentration) * 0.3333f;
        float effCompetitiveness = Mathf.Clamp01(this.aiCompetitiveness) * 0.3333f;

        return new AnimationCurve(new Keyframe(-1.0000f                     , -1.0f + effSkillLevel + aiCompetitiveness),
                                  new Keyframe(-0.3333f - effCompetitiveness,  0.0f + effSkillLevel),
                                  new Keyframe( 0.0000f                     ,  0.0f + effSkillLevel * 0.5f),
                                  new Keyframe( 0.3333f + effConcentration  ,  0.0f + effSkillLevel),
                                  new Keyframe( 1.0000f                     , -1.0f + effSkillLevel + aiConcentration)
                                 );
    }


    // Corrutinas
    protected IEnumerator AICorroutine()
    {
        while (this.alive)
        {
            // Esperar un tiempo
            float updateTimeMu = (this.aiUpdateTimeMin + this.aiUpdateTimeMax) * 0.5f;
            float updateTimeSigma = Mathf.Abs(this.aiUpdateTimeMax - this.aiUpdateTimeMin) * 0.25f;
            float updateTime = AIBehaviour.RandomGaussian(updateTimeMu, updateTimeSigma);
            yield return new WaitForSeconds(updateTime);

            // Actualizar estado de ánimo
			float advantage = 0;//AIBehaviour.ClampTo(this.CalculateAdvantage(), -1.0f, 1.0f);
            float emotionLimitFactor = advantage > 0.0f ? 0.5f - (this.aiEmotion * 0.5f) : 0.5f + (this.aiEmotion * 0.5f);

            float emotionDelta = advantage * this.aiEmotionalBias * updateTime;
            this.aiEmotion += emotionDelta * emotionLimitFactor;

            // Realizar intento
            float attempt = AIBehaviour.RandomGaussian(0, 0.25f);

            float emotionalSuccessRate = this.aiEmotionCurve.Evaluate(this.aiEmotion);
            float emotionalDeviation = AIBehaviour.RandomGaussian(emotionalSuccessRate, 0.25f);
            
            // Devolver resultado a través del evento
            this.OnAIOutput(AIBehaviour.ClampTo(attempt + emotionalDeviation, -1.0f, 1.0f));
        }
    }

}